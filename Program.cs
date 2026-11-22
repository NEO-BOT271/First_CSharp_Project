using Project_IT_STEP;
using Project_IT_STEP.ENUM;
using Project_IT_STEP.Menu;
using Project_IT_STEP.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;

ChooseLanguage();


void ChooseLanguage()
{
    UI.ShowMenu(["SELECT LANGUAGE  ›  ენის არჩევა"],
        "ქართული (Georgian)",
        "English");
    UI.CurrentLanguage = UI.Ask("Choose / არჩევანი:") == "1" ? Language.Georgian : Language.English;
}

var auth = new AuthService();
var moderators = new ModeratorService();
var adminMenu = new AdminMenu(auth, new AdminService(), moderators);
var providers = new ProviderService();
var ai = new AiService(providers);
var userMenu = new UserMenu(auth, new UserService(), ai);
var moderatorMenu = new ModeratorMenu(auth, moderators, providers);
while (true)
{
    UI.ShowMenu([Texts.AppName],
        Texts.LogInAsUser,
        Texts.LogInAsAdmin,
        Texts.LogInAsModerator,
        Texts.Exit,
        Texts.BackToLanguage);
    switch (UI.Ask(Texts.Choose))
    {
        case "1": userMenu.Run(); break;
        case "2": adminMenu.Run(); break;
        case "3": moderatorMenu.Run(); break;
        case "4": return;
        case "5": ChooseLanguage(); break;
        default: UI.Error(Texts.InvalidChoice); break;
    }
}