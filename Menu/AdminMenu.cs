using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Menu;

internal class AdminMenu(IAuthService auth, IAdminService admins, IModeratorService moderators)
    : RoleMenu<Admin>(auth, RoleSystem.Admin, "Admin Menu")
{
    protected override void SignUp() =>
        UI.Report(moderators.SubmitApplication(UI.ReadAccountRequest()),
            "Application sent. A moderator will review it.",
            "Invalid details, email already in use, or application already pending.");

    protected override void Open(Admin admin)
    {
        while (true)
        {
            UI.ShowMenu($"=== Admin: {admin.Name} ===", "Pending requests", "Price a request", "Log out");

            switch (UI.Ask("Choose:"))
            {
                case "1": UI.ShowRequests(admins.GetPendingRequests()); break;
                case "2": PriceRequest(); break;
                case "3": return;
                default: UI.Error("Invalid choice."); break;
            }
        }
    }

    private void PriceRequest()
    {
        var id = UI.AskNumber("Request id:");
        var price = decimal.TryParse(UI.Ask("Enter estimated price ($):"), out var parsed) ? parsed : 0;
        var notes = UI.Ask("Enter technician notes/instructions for the user:");
        UI.Report(admins.PriceRequest(id, price, notes),
            "Request priced.", "Request not found, not pending, or price invalid.");
    }
}