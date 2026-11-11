using Project_IT_STEP;
using Project_IT_STEP.Menu;
using Project_IT_STEP.Services;
using workingproject.Services;

var auth = new AuthService();
var moderators = new ModeratorService();
var userMenu = new UserMenu(auth, new UserService());
var adminMenu = new AdminMenu(auth, new AdminService(), moderators);
var moderatorMenu = new ModeratorMenu(auth, moderators);

while (true)
{
    UI.ShowMenu("=== IT SUPPORT ===",
        "Log in as user Menu",
        "Log in as admin Menu",
        "Log in as moderator",
        "Exit");

    switch (UI.Ask("Choose:"))
    {
        case "1": userMenu.Run(); break;
        case "2": adminMenu.Run(); break;
        case "3": moderatorMenu.Run(); break;
        case "4": return;
        default: UI.Error("Invalid choice."); break;
    }
}