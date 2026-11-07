using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Services;

public class AuthService : IAuthService
{
    private readonly JsonStorage<User> Users = new("users.json");
    private readonly JsonStorage<Admin> Admins = new("admins.json");
    private readonly JsonStorage<Moderator> Moderators = new("moderators.json");
    private readonly EmailRegistry Emails = new();
    public Person? LogIn(string email, string password, RoleSystem role)
    {
        var hash = PasswordHasher.Hash(password);
        return role switch
        {
            RoleSystem.Moderator => Find(Moderators.Load(), email, hash),
            RoleSystem.Admin => Find(Admins.Load(), email, hash),
            _ => Find(Users.Load(), email, hash)
        };
    }

    private static Person? Find(IEnumerable<Person> people, string email, string hash) =>
        people.FirstOrDefault(p =>
            string.Equals(p.Email, email, StringComparison.OrdinalIgnoreCase)
            && p.PasswordHash == hash);

    public bool CreateAccount(CreatingAccountRequest request)
    {
        if (!request.IsValid() || Emails.Exists(request.Email))
            return false;

        var users = Users.Load();
        users.Add(new User
        {
            Id = users.Count == 0 ? 1 : users.Max(u => u.Id) + 1,
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = PasswordHasher.Hash(request.Password)
        });
        Users.Save(users);
        return true;
        
    }
}