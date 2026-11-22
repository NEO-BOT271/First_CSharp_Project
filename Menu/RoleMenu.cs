using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;
using Project_IT_STEP.Services;

namespace Project_IT_STEP.Menu;

internal abstract class RoleMenu<T>(IAuthService auth, RoleSystem role, string title) where T : Person
{
    protected IAuthService Auth { get; } = auth;

    protected virtual bool CanSignUp => true;

    protected virtual void SignUp()
    {
    }

    protected abstract void Open(T person);

    public void Run()
    {
        string[] options = CanSignUp
            ? [Texts.LogIn, Texts.CreateAccount, Texts.Back]
            : [Texts.LogIn, Texts.Back];
        while (true)
        {
            UI.ShowMenu([Texts.AppName, title], options);
            var choice = UI.Ask(Texts.Choose);
            if (choice == "1")
                LogIn();
            else if (choice == "2" && CanSignUp)
                SignUp();
            else if (choice == options.Length.ToString())
                return;
            else
                UI.Error(Texts.InvalidChoice);
        }
    }

    private void LogIn()
    {
        if (Auth.LogIn(UI.Ask(Texts.Email), UI.AskPassword(Texts.Password), role) is not T person)
        {
            UI.Error(Texts.WrongLogin);
            return;
        }

        UI.Success(Texts.Welcome(person.Name));
        Open(person);
    }
}