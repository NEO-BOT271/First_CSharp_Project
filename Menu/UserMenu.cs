using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;
using Project_IT_STEP.Services;

namespace Project_IT_STEP.Menu;

internal class UserMenu(IAuthService auth, IUserService users, IAiService ai)
    : RoleMenu<User>(auth, RoleSystem.User, Texts.UserMenu)
{
    protected override void SignUp() =>
        UI.Report(Auth.CreateAccount(UI.ReadAccountRequest()),
            Texts.AccountCreated,
            Texts.InvalidDetails);

    protected override void Open(User user)
    {
        while (true)
        {
            UI.ShowMenu([Texts.AppName, Texts.User, user.Name], Texts.ReportTechnicalIssue, Texts.MyRequests, Texts.AcceptQuote, Texts.LogOut);

            switch (UI.Ask(Texts.Choose))
            {
                case "1": SubmitRequest(user); UI.Pause(); break;
                case "2": UI.ShowRequests(users.GetMyRequests(user.Id)); UI.Pause(); break;
                case "3": UI.Report(users.AcceptQuote(UI.AskNumber(Texts.RequestIdToAccept), user.Id),
                    Texts.QuoteAccept, Texts.NoPricedRequestWithThatId); break;
                case "4": return;
                default: UI.Error(Texts.InvalidChoice); break;
            }
        }
    }

    private void SubmitRequest(User user)
    {
        var category = SuggestFromAi() ?? ChooseCategory();
        if (category is null)
        {
            UI.Error(Texts.InvalidService);
            return;
        }

        var questions = QuestionsService.All[category.Value].Questions;
        var answers = questions.Select(UI.Ask).ToList();
        if (answers.Any(string.IsNullOrWhiteSpace))
        {
            UI.Error(Texts.AllAnswersAreRequired);
            return;
        }

        var details = string.Join(Environment.NewLine, questions.Zip(answers, (q, a) => $"{q} {a}"));
        var request = users.SubmitRequest(user.Id, new UserRequest { Category = category.Value, Details = details });
        UI.Success($"{Texts.Request} #{request.Id} {Texts.Sent}. {Texts.Status} {Texts.StatusName(request.Status)}");
        
    }

    private static ServiceCategory? ChooseCategory()
    {
        foreach (var (category, service) in QuestionsService.All)
            UI.Option($"{(int)category}. {service.Title}");

        return int.TryParse(UI.Ask(Texts.ChooseService), out var number) && Enum.IsDefined((ServiceCategory)number)
            ? (ServiceCategory)number
            : null;
    }
    
    private ServiceCategory? SuggestFromAi()
    {
        var problem = UI.Ask(Texts.DescribeProblem);
        if (string.IsNullOrWhiteSpace(problem))
            return null;

        var suggestion = ai.SuggestCategory(problem);
        if (suggestion is null)
            return null;

        return UI.Ask($"{Texts.SuggestedService} {(int)suggestion}. {QuestionsService.All[suggestion.Value].Title}. {Texts.UseThis}").ToLower() == "y"
            ? suggestion
            : null;
    }
}