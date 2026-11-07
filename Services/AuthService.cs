using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Services;

public class AuthService : IAuthService
{
    private readonly JsonStorage<User> Users = new("users.json");
    private readonly JsonStorage<Admin> Admins = new("admins.json");
    private readonly JsonStorage<Moderator> Moderators = new("moderators.json");

    public Person? LogIn(string email, string password)
    {
        var hash = PasswordHasher.Hash(password);
        return Find(Moderators.Load(), email, hash)
               ?? Find(Admins.Load(), email, hash)
               ?? Find(Users.Load(), email, hash);
    }

    private static Person? Find(IEnumerable<Person> people, string email, string hash) =>
        people.FirstOrDefault(p =>
            string.Equals(p.Email, email, StringComparison.OrdinalIgnoreCase)
            && p.PasswordHash == hash);

    public bool CreateAccount(CreatingAccountRequest request)
    {
        throw new NotImplementedException();
    }
}