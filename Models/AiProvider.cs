using Project_IT_STEP.ENUM;

namespace Project_IT_STEP.Models;

internal class AiProvider
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public ProviderType Type { get; set; }
    public string Url { get; set; } = "";
    public string ApiKey { get; set; } = "";
    public bool IsLocal { get; set; }
    public string Model { get; set; } = "";
    public bool IsActive { get; set; }
}