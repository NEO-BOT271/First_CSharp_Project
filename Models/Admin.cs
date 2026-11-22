using Project_IT_STEP.ENUM;

namespace Project_IT_STEP.Models;

internal class Admin : Person
{
    public override RoleSystem Role => RoleSystem.Admin;
    public string WorksAt { get; set; } = "";
    public string Certificate { get; set; } = "";
    public string Experience { get; set; } = "";
}
