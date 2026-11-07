using Project_IT_STEP.Models;

namespace Project_IT_STEP.Services;

public class EmailRegistry
{
    private readonly JsonStorage<User> Users = new("users.json");
    private readonly JsonStorage<Admin> Admins = new("admins.json");
    private readonly JsonStorage<Moderator> Moderators = new("moderators.json");

    public bool Exists(string email) =>
        Users.Load().Cast<Person>()
            .Concat(Admins.Load())
            .Concat(Moderators.Load())
            .Any(p => string.Equals(p.Email, email, StringComparison.OrdinalIgnoreCase));
}