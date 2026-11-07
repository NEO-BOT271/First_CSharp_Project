using Project_IT_STEP.Models;

namespace Project_IT_STEP.Interfaces;

internal interface IModeratorService
{
    bool HireAdmin(CreatingAccountRequest request);
    List<Admin> GetAdmins();
    bool RemoveAdmin(int adminId);
}