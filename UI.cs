using System.Text;
using Project_IT_STEP.ENUM;
using Project_IT_STEP.Models;
using Project_IT_STEP.Services;

namespace Project_IT_STEP;

internal static class UI
{
    public static void Title(string text) => Write(text, ConsoleColor.Cyan);
    public static void Option(string text) => Write(text, ConsoleColor.Gray);
    public static void Info(string text) => Write(text, ConsoleColor.DarkGray);
    public static void Success(string text) => Notify(text, ConsoleColor.Green);
    public static void Error(string text) => Notify(text, ConsoleColor.Red);
    public static Language CurrentLanguage { get; set; } = Language.English;
    
    private static string? PendingMessage;
    private static ConsoleColor PendingColor;

    
    public static void Header(params string[] path)
    {
        var text = $" {string.Join("  ›  ", path)} ";
        var width = Math.Max(40, text.Length + 2);
        var line = new string('═', width);

        Title($"╔{line}╗");
        Title($"║{text.PadRight(width)}║");
        Title($"╚{line}╝");
    }
    public static (string WorksAt, string Certificate, string Experience) ReadAdminQuestions()
    {
        var worksAt = Ask(Texts.WorksAtQuestion);
        var certificate = Ask(Texts.CertificateQuestion);
        var experience = Ask(Texts.ExperienceQuestion);
        return (worksAt, certificate, experience);
    }
    public static void Pause()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(Texts.PressEnterToReturn);    
        Console.ResetColor();
        Console.ReadLine();
    }
    
    public static void Notify(string text, ConsoleColor color)
    {
        PendingMessage = text;
        PendingColor = color;
    }
    public static string Ask(string question)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(question + " ");
        Console.ResetColor();
        return Console.ReadLine()?.Trim() ?? "";
    }

    public static int AskNumber(string question) =>
        int.TryParse(Ask(question), out var number) ? number : 0;

    public static void ShowMenu(string[] path, params string[] options)
    {
        Console.Clear();
        if (PendingMessage is not null)
        {
            Console.ForegroundColor = PendingColor;
            Console.WriteLine(PendingMessage);
            Console.ResetColor();
            PendingMessage = null;
        }
        Header(path);
        for (var i = 0; i < options.Length; i++)
        {
            Option($"{i + 1}. {options[i]}");
        }
    }

    public static void Report(bool succeeded, string success, string failure)
    {
        if (succeeded)
        {
            Success(success);
        }
        else
        {
            Error(failure);
        }
    }

    public static CreatingAccountRequest ReadAccountRequest()
    {
        var request = new CreatingAccountRequest
        {
            Name = Ask(Texts.Name),
            LastName = Ask(Texts.LastName),
            Email = Ask(Texts.Email)
        };

        Info(Texts.PasswordRule);
        request.Password = AskPassword(Texts.Password);
        return request;
    }

    public static void ShowRequests(List<UserRequest> requests)
    {
        if (requests.Count == 0)
            Info(Texts.NoRequests);

        foreach (var request in requests)
        {
            Title($"#{request.Id} {QuestionsService.All[request.Category].Title} - {Texts.StatusName(request.Status)}");
            Info(request.Details);
            if (request.Price is not null)
                Info($"{Texts.PriceLabel} ${request.Price}   {Texts.NotesLabel} {request.TechnicianNotes}");
        }
    }

    public static void ShowList<T>(List<T> items, Func<T, string> describe)
    {
        if (items.Count == 0)
            Info(Texts.NothingToShow);

        foreach (var item in items)
            Info(describe(item));
    }

    private static void Write(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
    public static string AskPassword(string question)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(question + " ");
        Console.ResetColor();

        var password = new StringBuilder();
        while (true)
        {
            var key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Enter)
                break;
            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password.Length--;
                Console.Write("\b \b");
            }
            else if (!char.IsControl(key.KeyChar))
            {
                password.Append(key.KeyChar);
                Console.Write('*');
            }
        }

        Console.WriteLine();
        return password.ToString();
    }
}