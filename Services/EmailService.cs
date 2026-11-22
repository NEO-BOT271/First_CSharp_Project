using System.Net;
using System.Net.Mail;
using System.Text.Json;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Services;

internal class EmailService : IEmailService
{
    private readonly EmailSettings Settings = LoadSettings();
    private readonly JsonStorage<Moderator> Moderators = new("moderators.json");
    private readonly JsonStorage<Admin> Admins = new("admins.json");
    private readonly JsonStorage<User> Users = new("users.json");
    public bool Send(string toEmail, string subject, string htmlBody)
    {
        try
        {
            using var message = new MailMessage(Settings.SenderEmail, toEmail, subject, htmlBody)
            {
                IsBodyHtml = true,
                SubjectEncoding = System.Text.Encoding.UTF8,
                BodyEncoding = System.Text.Encoding.UTF8
            };
            using var client = new SmtpClient(Settings.Host, Settings.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(Settings.SenderEmail, Settings.AppPassword)
            };
            client.Send(message);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool SendToAdmins(string subject, string htmlBody) =>
        SendToAll(Admins.Load(), subject, htmlBody);
    
    
    
    public bool SendToModerators(string subject, string htmlBody) =>
        SendToAll(Moderators.Load(), subject, htmlBody);
    public bool SendToUser(int userId, string subject, string htmlBody)
    {
        var user = Users.Load().FirstOrDefault(u => u.Id == userId);
        return user is not null && Send(user.Email, subject, htmlBody);
    }
    
    private bool SendToAll(IEnumerable<Person> people, string subject, string htmlBody)
    {
        var allSent = true;
        foreach (var person in people)
            allSent &= Send(person.Email, subject, htmlBody);
        return allSent;
    }

    private static EmailSettings LoadSettings()
    {
        var path = System.IO.Path.Combine(AppContext.BaseDirectory, "Data", "emailsettings.json");
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        return JsonSerializer.Deserialize<EmailSettings>(stream) ?? new EmailSettings();
    }
}