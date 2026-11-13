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

    public static string ApplicationReceived(string name, string lastName, string email) =>
        Fill("New admin application",
            $"<p>{Enc(name)} {Enc(lastName)} applied to become an admin.</p>" +
            $"<p>Email: <b>{Enc(email)}</b></p>" +
            "<p>Log in as moderator to approve or reject it.</p>");

    public static string RequestReceived(int requestId, int userId, string service) =>
        Fill($"New request #{requestId}",
            $"<p>User #{userId} submitted a request for <b>{Enc(service)}</b>.</p>" +
            "<p>Log in as admin to review and price it.</p>");

    private static string Fill(string title, string body) =>
        Layout.Replace("{{Title}}", Enc(title)).Replace("{{Body}}", body);
    
    public static string QuotePriced(int requestId, decimal price, string notes) =>
        Fill($"Your request #{requestId} has been priced",
            $"<p>Estimated price: <b>${price}</b></p>" +
            $"<p>Technician notes: {Enc(notes)}</p>" +
            "<p>Log in as user to accept the quote.</p>");
    private static string Enc(string text) => WebUtility.HtmlEncode(text);
}