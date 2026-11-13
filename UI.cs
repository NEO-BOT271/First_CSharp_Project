using System.Text;
using Project_IT_STEP.Models;
using Project_IT_STEP.Services;

namespace Project_IT_STEP;

internal static class UI
{
    public static void Title(string text) => Write(text, ConsoleColor.Cyan);
    public static void Option(string text) => Write(text, ConsoleColor.Gray);
    public static void Info(string text) => Write(text, ConsoleColor.DarkGray);
    public static void Success(string text) => Write(text, ConsoleColor.Green);
    public static void Error(string text) => Write(text, ConsoleColor.Red);

    public static string Ask(string question)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(question + " ");
        Console.ResetColor();
        return Console.ReadLine()?.Trim() ?? "";
    }

    public static int AskNumber(string question) =>
        int.TryParse(Ask(question), out var number) ? number : 0;

    public static void ShowMenu(string title, params string[] options)
    {
        Console.Clear();
        Title(title);
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
        Info("Password must be 8 to 15 characters.");
        return new CreatingAccountRequest
        {
            Name = Ask("Name:"),
            LastName = Ask("Last name:"),
            Email = Ask("Email:"),
            Password = AskPassword("Password:")
        };
    }

    public static void ShowRequests(List<UserRequest> requests)
    {
        if (requests.Count == 0)
            Info("No requests.");

        foreach (var request in requests)
        {
            Title($"#{request.Id} {QuestionsService.All[request.Category].Title} - {request.Status}");
            Info(request.Details);
            if (request.Price is not null)
                Info($"Price: ${request.Price}   Notes: {request.TechnicianNotes}");
        }
    }

    public static void ShowList<T>(List<T> items, Func<T, string> describe)
    {
        if (items.Count == 0)
            Info("Nothing to show.");

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