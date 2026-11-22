using System.Net.Http.Headers;
using System.Text.Json;
using Project_IT_STEP.ENUM;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Services;

internal static class ModelFetcher
{
    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(10) };

    public static List<string> Fetch(AiProvider provider)
    {
        try
        {
            var url = provider.Type == ProviderType.Gemini
                ? $"https://generativelanguage.googleapis.com/v1beta/models?key={provider.ApiKey}"
                : provider.IsLocal
                    ? $"{provider.Url.TrimEnd('/')}/api/tags"
                    : $"{provider.Url.TrimEnd('/')}/v1/models";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            if (provider.Type == ProviderType.OpenAiCompatible && !string.IsNullOrEmpty(provider.ApiKey))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", provider.ApiKey);

            using var response = Http.Send(request);
            if (!response.IsSuccessStatusCode)
                return [];

            using var stream = response.Content.ReadAsStream();
            using var json = JsonDocument.Parse(stream);
            return provider.Type == ProviderType.Gemini ? ParseGemini(json.RootElement)
                : provider.IsLocal ? ParseOllama(json.RootElement)
                : ParseOpenAi(json.RootElement);
        }
        catch (Exception)
        {
            return [];
        }
    }

    private static List<string> ParseGemini(JsonElement root) =>
        root.GetProperty("models").EnumerateArray()
            .Where(m => m.TryGetProperty("supportedGenerationMethods", out var methods)
                        && methods.EnumerateArray().Any(x => x.GetString() == "generateContent"))
            .Select(m => m.GetProperty("name").GetString() ?? "")
            .Select(n => n.Replace("models/", ""))
            .Where(n => n.Length > 0)
            .ToList();

    private static List<string> ParseOpenAi(JsonElement root) =>
        root.GetProperty("data").EnumerateArray()
            .Select(m => m.GetProperty("id").GetString() ?? "")
            .Where(id => id.Length > 0)
            .ToList();
    private static List<string> ParseOllama(JsonElement root) =>
        root.GetProperty("models").EnumerateArray()
            .Select(m => m.GetProperty("name").GetString() ?? "")
            .Where(n => n.Length > 0)
            .ToList();
}