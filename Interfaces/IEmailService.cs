namespace Project_IT_STEP.Interfaces;

internal interface IEmailService
{
    public bool Send(string toEmail, string subject, string htmlBody);
    bool SendToModerators(string subject, string htmlBody);
    bool SendToAdmins(string subject, string htmlBody);
    bool SendToUser(int userId, string subject, string htmlBody);
}