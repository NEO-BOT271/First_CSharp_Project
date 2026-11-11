using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;
using Project_IT_STEP.Services;

namespace Project_IT_STEP.Menu;

internal class UserMenu(IAuthService auth, IUserService users)
    : RoleMenu<User>(auth, RoleSystem.User, "User Menu")
{
    protected override void SignUp() =>
        UI.Report(Auth.CreateAccount(UI.ReadAccountRequest()),
            "Account created. You can log in now.",
            "Invalid details or email already in use.");

    protected override void Open(User user)
    {
        while (true)
        {
            UI.ShowMenu($"=== User: {user.Name} ===",
                "Submit a request", "My requests", "Accept a quote", "Log out");

            switch (UI.Ask("Choose:"))
            {
                case "1": SubmitRequest(user); break;
                case "2": UI.ShowRequests(users.GetMyRequests(user.Id)); break;
                case "3": UI.Report(users.AcceptQuote(UI.AskNumber("Request id to accept:"), user.Id),
                    "Quote accepted.", "No priced request with that id."); break;
                case "4": return;
                default: UI.Error("Invalid choice."); break;
            }
        }
    }

    private void SubmitRequest(User user)
    {
        var category = ChooseCategory();
        if (category is null)
        {
            UI.Error("Invalid service.");
            return;
        }

        var questions = QuestionsService.All[category.Value].Questions;
        var answers = questions.Select(UI.Ask).ToList();
        if (answers.Any(string.IsNullOrWhiteSpace))
        {
            UI.Error("All answers are required.");
            return;
        }

        var details = string.Join(Environment.NewLine, questions.Zip(answers, (q, a) => $"{q} {a}"));
        var request = users.SubmitRequest(user.Id, new UserRequest { Category = category.Value, Details = details });
        UI.Success($"Request #{request.Id} sent. Status: {request.Status}.");
    }

    private static ServiceCategory? ChooseCategory()
    {
        foreach (var (category, service) in QuestionsService.All)
            UI.Option($"{(int)category}. {service.Title}");

        return int.TryParse(UI.Ask("Choose a service:"), out var number) && Enum.IsDefined((ServiceCategory)number)
            ? (ServiceCategory)number
            : null;
    }
}