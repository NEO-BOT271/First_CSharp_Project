using System.Net;

namespace Project_IT_STEP.Services;

internal static class EmailTemplates
{
    public const string Layout = """
        <!DOCTYPE html>
        <html>
        <head>
          <meta charset="UTF-8">
          <style>
            body { font-family: Segoe UI, Tahoma, Arial, sans-serif; background: #f4f6fb; color: #333; margin: 0; padding: 20px; }
            .box { max-width: 480px; margin: auto; background: #fff; padding: 24px; border-radius: 8px; }
            h2 { color: #4169e1; margin-top: 0; }
            .footer { font-size: 12px; color: #888; border-top: 1px solid #eee; padding-top: 12px; }
          </style>
        </head>
        <body>
          <div class="box">
            <h2>{{Title}}</h2>
            {{Body}}
            <p class="footer">Automated message, please do not reply.</p>
          </div>
        </body>
        </html>
        """;

    public const string ApplicationSubject = "New admin application";

    public static string RequestSubject(int requestId) => $"New request #{requestId}";

    public static string QuoteSubject(int requestId) => $"Your request #{requestId} has been priced";

    public const string AdminApprovedSubject = "Your admin application was approved";

    public static string ApplicationReceived(string name, string lastName, string email,
        string worksAt, string certificate, string experience) =>
        Fill(ApplicationSubject,
            $"<p>{Enc(name)} {Enc(lastName)} applied to become an admin.</p>" +
            $"<p>Email: <b>{Enc(email)}</b></p>" +
            $"<p>Works at: {Enc(worksAt)}</p>" +
            $"<p>Certificate: {Enc(certificate)}</p>" +
            $"<p>Experience: {Enc(experience)}</p>" +
            "<p>Log in as moderator to approve or reject it.</p>");

    public static string RequestReceived(int requestId, int userId, string service) =>
        Fill(RequestSubject(requestId),
            $"<p>User #{userId} submitted a request for <b>{Enc(service)}</b>.</p>" +
            "<p>Log in as admin to review and price it.</p>");

    public static string QuotePriced(int requestId, decimal price, string notes) =>
        Fill(QuoteSubject(requestId),
            $"<p>Estimated price: <b>${price}</b></p>" +
            $"<p>Technician notes: {Enc(notes)}</p>" +
            "<p>Log in as user to accept the quote.</p>");

    public static string AdminApproved(string name) =>
        Fill(AdminApprovedSubject,
            $"<p>Hello {Enc(name)},</p>" +
            "<p>Your admin application has been approved. You can now log in as admin.</p>");

    private static string Fill(string title, string body) =>
        Layout.Replace("{{Title}}", Enc(title)).Replace("{{Body}}", body);

    private static string Enc(string text) => WebUtility.HtmlEncode(text);
}