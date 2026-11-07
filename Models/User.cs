using Project_IT_STEP.ENUM;

namespace Project_IT_STEP.Models;

internal class User : Person
{
    public override RoleSystem Role => RoleSystem.User;
}