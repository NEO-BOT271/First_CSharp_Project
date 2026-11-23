using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;
using Project_IT_STEP.Services;

namespace Project_IT_STEP.Menu;

internal class ModeratorMenu(IAuthService auth, IModeratorService moderators, IProviderService providers)
    : RoleMenu<Moderator>(auth, RoleSystem.Moderator, Texts.Moderator)
{
    protected override bool CanSignUp => false;

    protected override void Open(Moderator moderator)
    {
        while (true)
        {
            UI.ShowMenu([Texts.AppName, Texts.Moderator, moderator.Name],
                Texts.HireAdmin, Texts.ListAdmin, Texts.RemoveAdmin,
                Texts.PendingApplication, Texts.ReviewApplication,
                Texts.AiConfig, Texts.LogOut);

            switch (UI.Ask(Texts.Choose))
            {
                case "1": UI.Report(moderators.HireAdmin(UI.ReadAccountRequest()),
                    Texts.AdminHired, Texts.InvalidDetails); break;
                case "2": UI.ShowList(moderators.GetAdmins(),
                    a => $"#{a.Id} {a.Name} {a.LastName} - {a.Email}"); UI.Pause(); break;
                case "3": UI.Report(moderators.RemoveAdmin(UI.AskNumber(Texts.AdminId)),
                    Texts.AdminRemoved, Texts.AdminWithNoId); break;
                case "4": UI.ShowList(moderators.GetPendingApplications(),
                    a => $"#{a.Id} {a.Name} {a.LastName} - {a.Email}\n" +
                         $"    {Texts.WorksAtLabel} {a.WorksAt}\n" +
                         $"    {Texts.CertificateLabel} {a.Certificate}\n" +
                         $"    {Texts.ExperienceLabel} {a.Experience}"); UI.Pause(); break;
                case "5": ReviewApplication(); UI.Pause(); break;
                case "6": new AiConfigMenu(providers).Run(); break;
                case "7": return;
                default: UI.Error(Texts.InvalidChoice); break;
            }
        }
    }

    private void ReviewApplication()
    {
        var id = UI.AskNumber(Texts.ApplicationId);
        var answer = UI.Ask(Texts.ApproveReject).ToLower();
        if (answer is not ("y" or "n"))
        {
            UI.Error(Texts.ApproveRejectElse);
            return;
        }

        var approve = answer == "y";
        UI.Report(moderators.ReviewApplication(id, approve),
            approve ? Texts.AdminApproved : Texts.ApplicationReject,
            Texts.ApplicationNoPending);
    }
}