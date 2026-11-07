using Project_IT_STEP.ENUM;

namespace Project_IT_STEP.Models;

public abstract class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public abstract RoleSystem Role { get; }
}