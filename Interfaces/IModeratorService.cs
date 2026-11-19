using Project_IT_STEP.Models;

namespace Project_IT_STEP.Interfaces;

internal interface IModeratorService
{
    bool HireAdmin(CreatingAccountRequest request);
    List<Admin> GetAdmins();
    bool RemoveAdmin(int adminId);
    bool SubmitApplication(CreatingAccountRequest request, string worksAt, string certificate, string experience);
    List<AdminApplication> GetPendingApplications();
    bool ReviewApplication(int applicationId, bool approve);
}