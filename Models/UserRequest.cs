using Project_IT_STEP.ENUM;

namespace Project_IT_STEP.Models;

internal class UserRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Problem { get; set; } = "";
    public string ContactInfo { get; set; } = "";
    public UserRequestStatus Status { get; set; } = UserRequestStatus.Pending;
    public decimal? Price { get; set; }
    public string? TechnicianNotes { get; set; }
}