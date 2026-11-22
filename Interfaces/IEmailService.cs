namespace Project_IT_STEP.Interfaces;

internal interface IEmailService
{
    bool Send(string toEmail, string subject, string htmlBody);
    bool SendToModerators(string subject, string htmlBody);
    bool SendToAdmins(string subject, string htmlBody);
    bool SendToUser(int userId, string subject, string htmlBody);
}