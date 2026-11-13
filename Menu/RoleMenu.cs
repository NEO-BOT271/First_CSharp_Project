using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Menu;

internal abstract class RoleMenu<T>(IAuthService auth, RoleSystem role, string title) where T : Person
{
    protected IAuthService Auth { get; } = auth;

    protected virtual bool CanSignUp => true;
    protected virtual void SignUp() { }
    protected abstract void Open(T person);

    public void Run()
    {
        string[] options = CanSignUp ? ["Log in", "Create account", "Back"] : ["Log in", "Back"];
        while (true)
        {
            UI.ShowMenu($"=== {title} ===", options);
            var choice = UI.Ask("Choose:");
            if (choice == "1")
                LogIn();
            else if (choice == "2" && CanSignUp)
                SignUp();
            else if (choice == options.Length.ToString())
                return;
            else
                UI.Error("Invalid choice.");
        }
    }

    private void LogIn()
    {
        if (Auth.LogIn(UI.Ask("Email:"), UI.AskPassword("Password:"), role) is not T person)
        {
            UI.Error("Wrong email or password.");
            return;
        }

        UI.Success($"Welcome, {person.Name}!");
        Open(person);
    }
}