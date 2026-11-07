using Project_IT_STEP.ENUM;

namespace Project_IT_STEP.Models;

public class AdminApplication
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
}