using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Services;

internal class AiService(IProviderService providers) : IAiService
{
    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(15) };

    public ServiceCategory? SuggestCategory(string problem)
    {
        var provider = providers.GetActive();
        if (provider is null || string.IsNullOrWhiteSpace(provider.Model))
            return null;

        try
        {
            var reply = Send(provider, Prompt(problem));
            return int.TryParse(reply.Trim(), out var number) && number is >= 1 and <= 6
                ? (ServiceCategory)number
                : null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static string Prompt(string problem) =>
        "Pick the best category number for this IT problem. Reply with only the number.\n" +
        "Categories: 1 Equipment Repair, 2 Wi-Fi & Network Security, 3 Hardware Installation, " +
        "4 OS & Software Installation, 5 Remote Support, 6 Data Recovery.\n" +
        $"Problem: {problem}";

    private static string Send(AiProvider provider, string prompt)
    {
        HttpRequestMessage request;
        if (provider.Type == ProviderType.Gemini)
        {
            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/{provider.Model}:generateContent?key={provider.ApiKey}";
            var body = new { contents = new[] { new { parts = new[] { new { text = prompt } } } } };
            request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };
        }
        else
        {
            var url = $"{provider.Url.TrimEnd('/')}/v1/chat/completions";
            var body = new { model = provider.Model, messages = new[] { new { role = "user", content = prompt } } };
            request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };
            if (!string.IsNullOrEmpty(provider.ApiKey))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", provider.ApiKey);
        }

        using var response = Http.Send(request);
        if (!response.IsSuccessStatusCode)
            return "";

        using var json = JsonDocument.Parse(response.Content.ReadAsStream());
        var root = json.RootElement;
        return provider.Type == ProviderType.Gemini
            ? root.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text")
                .GetString() ?? ""
            : root.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "";
    }

    private static readonly string[] Topics =
    [
        "where they work or worked before",
        "whether they have a certificate (free text)",
        "which IT areas they have experience in"
    ];

    public string? NextQuestion(List<string> answers)
    {
        var provider = providers.GetActive();
        if (provider is null || string.IsNullOrWhiteSpace(provider.Model) || answers.Count >= Topics.Length)
            return null;

        var prompt = $"You are interviewing a candidate for an IT support admin job. " +
                     $"Ask one short question about: {Topics[answers.Count]}. Reply with only the question.";
        try
        {
            var reply = Send(provider, prompt).Trim();
            return reply.Length > 0 ? reply : null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public string? Summarize(List<string> answers)
    {
        var provider = providers.GetActive();
        if (provider is null || string.IsNullOrWhiteSpace(provider.Model))
            return null;

        var prompt = "Summarize this IT admin candidate in two short lines for a moderator. " +
                     "Use only the facts given.\n" + string.Join("\n", answers);
        try
        {
            var reply = Send(provider, prompt).Trim();
            return reply.Length > 0 ? reply : null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}