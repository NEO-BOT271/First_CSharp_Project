using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Services;

public class AuthService : IAuthService
{
    public Person? LogIn(string email, string password)
    {
        throw new NotImplementedException();
    }

    public bool CreateAccount(CreatingAccountRequest request)
    {
        throw new NotImplementedException();
    }
}