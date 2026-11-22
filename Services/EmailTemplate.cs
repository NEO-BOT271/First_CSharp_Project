using System.Net;

namespace Project_IT_STEP.Services;

internal static class EmailTemplates
{
    public const string Layout = """
                                 <!DOCTYPE html>
                                 <html>
                                 <head>
                                   <meta charset="UTF-8">
                                   <meta name="viewport" content="width=device-width, initial-scale=1">
                                 </head>
                                 <body style="margin:0; padding:0; background:#eef1f7; font-family:Segoe UI, Tahoma, Arial, sans-serif;">
                                   <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="background:#eef1f7; padding:32px 12px;">
                                     <tr>
                                       <td align="center">
                                         <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="max-width:520px; background:#ffffff; border-radius:12px; overflow:hidden;">
                                           <tr>
                                             <td style="background:#1f3b73; padding:20px 28px; color:#ffffff;">
                                               <div style="font-size:12px; letter-spacing:2px; opacity:0.8;">IT SUPPORT</div>
                                               <div style="font-size:20px; font-weight:600; margin-top:4px;">{{Title}}</div>
                                             </td>
                                           </tr>
                                           <tr>
                                             <td style="padding:28px; color:#333333; font-size:15px; line-height:1.6;">
                                               {{Body}}
                                             </td>
                                           </tr>
                                           <tr>
                                             <td style="padding:16px 28px; background:#f7f9fc; color:#888888; font-size:12px; border-top:1px solid #e6eaf2;">
                                               Automated message, please do not reply.
                                             </td>
                                           </tr>
                                         </table>
                                       </td>
                                     </tr>
                                   </table>
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