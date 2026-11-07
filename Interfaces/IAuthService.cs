using Project_IT_STEP.Models;

namespace Project_IT_STEP.Interfaces;

internal interface IAuthService
{
    Person? LogIn(string email, string password);
    bool CreateAccount(CreatingAccountRequest request);
}