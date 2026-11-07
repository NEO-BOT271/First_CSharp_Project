namespace Project_IT_STEP.Models;

public class CreatingAccountRequest
{
    public string Name { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    
    public bool IsValid() =>
        !string.IsNullOrWhiteSpace(Name)
        && !string.IsNullOrWhiteSpace(LastName)
        && !string.IsNullOrWhiteSpace(Email)
        && Password.Length is >= 8 and <= 15;
}