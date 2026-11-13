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
    public bool Send(string toEmail, string subject, string body)
    {
        try
        {
            using var message = new MailMessage(Settings.SenderEmail, toEmail, subject, body)
            {
                IsBodyHtml = true
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

    public bool SendToAdmins(string subject, string htmlBody)
    {
        var allSent = true;
        foreach (var admin in Admins.Load())
            allSent &= Send(admin.Email, subject, htmlBody);
        return allSent;
    }
    
    public bool SendToModerators(string subject, string htmlBody)
    {
        var allSent = true;
        foreach (var moderator in Moderators.Load())
            allSent &= Send(moderator.Email, subject, htmlBody);
        return allSent;
    }
    public bool SendToUser(int userId, string subject, string htmlBody)
    {
        var user = Users.Load().FirstOrDefault(u => u.Id == userId);
        return user is not null && Send(user.Email, subject, htmlBody);
    }

    private static EmailSettings LoadSettings()
    {
        var path = System.IO.Path.Combine(AppContext.BaseDirectory, "Data", "emailsettings.json");
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        return JsonSerializer.Deserialize<EmailSettings>(stream) ?? new EmailSettings();
    }
}