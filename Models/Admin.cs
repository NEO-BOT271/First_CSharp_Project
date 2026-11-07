using Project_IT_STEP.ENUM;

namespace Project_IT_STEP.Models;

    public class Admin : Person
    {
        public override RoleSystem Role => RoleSystem.Admin;
    }
