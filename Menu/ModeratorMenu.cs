using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Menu;

internal class ModeratorMenu(IAuthService auth, IModeratorService moderators)
    : RoleMenu<Moderator>(auth, RoleSystem.Moderator, "Moderator")
{
    protected override bool CanSignUp => false;

    protected override void Open(Moderator moderator)
    {
        while (true)
        {
            UI.ShowMenu($"=== Moderator: {moderator.Name} ===",
                "Hire an admin", "List admins", "Remove an admin",
                "Pending applications", "Review an application", "Log out");

            switch (UI.Ask("Choose:"))
            {
                case "1": UI.Report(moderators.HireAdmin(UI.ReadAccountRequest()),
                    "Admin hired.", "Invalid details or email already in use."); break;
                case "2": UI.ShowList(moderators.GetAdmins(),
                    a => $"#{a.Id} {a.Name} {a.LastName} - {a.Email}"); break;
                case "3": UI.Report(moderators.RemoveAdmin(UI.AskNumber("Admin id:")),
                    "Admin removed.", "No admin with that id."); break;
                case "4": UI.ShowList(moderators.GetPendingApplications(),
                    a => $"#{a.Id} {a.Name} {a.LastName} - {a.Email}"); break;
                case "5": ReviewApplication(); break;
                case "6": return;
                default: UI.Error("Invalid choice."); break;
            }
        }
    }

    private void ReviewApplication()
    {
        var id = UI.AskNumber("Application id:");
        var answer = UI.Ask("Approve (y) or reject (n)?").ToLower();
        if (answer is not ("y" or "n"))
        {
            UI.Error("Type y or n.");
            return;
        }

        var approve = answer == "y";
        UI.Report(moderators.ReviewApplication(id, approve),
            approve ? "Admin approved and created." : "Application rejected.",
            "No pending application with that id, or email already registered.");
    }
}