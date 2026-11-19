using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;
using Project_IT_STEP.Services;

namespace Project_IT_STEP.Menu;

internal class AdminMenu(IAuthService auth, IAdminService admins, IModeratorService moderators)
    : RoleMenu<Admin>(auth, RoleSystem.Admin, Texts.AdminMenu)
{
  
    protected override void SignUp()
    {
        var request = UI.ReadAccountRequest();
        var (worksAt, certificate, experience) = UI.ReadAdminQuestions();
        UI.Report(moderators.SubmitApplication(request, worksAt, certificate, experience),
            Texts.ApplicationSent,
            Texts.InvalidDetailsAdmin);
    }

    protected override void Open(Admin admin)
    {
        while (true)
        {
            UI.ShowMenu([Texts.AppName, Texts.Admin, admin.Name],
                Texts.PendingRequests,
                Texts.PriceARequest,
                Texts.LogOut);

            switch (UI.Ask(Texts.Choose))
            {
                case "1":
                    UI.ShowRequests(admins.GetPendingRequests());
                    UI.Pause();
                    break;
                case "2":
                    PriceRequest();
                    UI.Pause();
                    break;
                case "3": return;
                default: UI.Error(Texts.InvalidChoice); break;
            }
        }
    }

    private void PriceRequest()
    {
        var id = UI.AskNumber(Texts.RequestId);
        var price = decimal.TryParse(UI.Ask(Texts.EstimatedPrice), out var parsed) ? parsed : 0;
        var notes = UI.Ask(Texts.TechnicianNotes);
        UI.Report(admins.PriceRequest(id, price, notes),
            Texts.RequestPriced, Texts.RequestNotFound);
    }
}