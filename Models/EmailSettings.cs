namespace Project_IT_STEP.Models;

internal class EmailSettings
{
    public string Host { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public string SenderEmail { get; set; } = "";
    public string AppPassword { get; set; } = "";
}