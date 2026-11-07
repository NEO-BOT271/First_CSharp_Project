using Project_IT_STEP.ENUM;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Interfaces;

internal interface IAuthService
{
    Person? LogIn(string email, string password, RoleSystem role);
    bool CreateAccount(CreatingAccountRequest request);
}