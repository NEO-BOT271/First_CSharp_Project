namespace Project_IT_STEP;

public class UI
{
    public static void Title(string text) => Write(text, ConsoleColor.Cyan);
    public static void Option(string text) => Write(text, ConsoleColor.Magenta);
    public static void Info(string text) => Write(text, ConsoleColor.Gray);
    public static void Success(string text) => Write(text, ConsoleColor.Green);
    public static void Error(string text) => Write(text, ConsoleColor.Red);

    public static string Ask(string question)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(question + " ");
        Console.ResetColor();
        return Console.ReadLine()?.Trim() ?? "";
    }

    private static void Write(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}