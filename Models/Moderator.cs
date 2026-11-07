using Project_IT_STEP.ENUM;

namespace Project_IT_STEP.Models;

internal class Moderator : Person
{
    public override RoleSystem Role => RoleSystem.Moderator;    
}