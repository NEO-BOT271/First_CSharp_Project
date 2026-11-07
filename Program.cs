using Project_IT_STEP;
using Project_IT_STEP.ENUM;
using Project_IT_STEP.Models;
using Project_IT_STEP.Services;
using workingproject.Services;

class Program
{
    static void Main()
    {
        var auth = new AuthService();
        var moderators = new ModeratorService();
        var users = new UserService();
        var admins = new AdminService();

        while (true)
        {
            ShowMenu("=== IT SUPPORT ===",
                "Log in as user",
                "Create account as user",
                "Log in as admin",
                "Create account as admin",
                "Log in as moderator",
                "Exit");

            switch (UI.Ask("Choose:"))
            {
                case "1": LogIn(RoleSystem.User); break;
                case "2": CreateUserAccount(); break;
                case "3": LogIn(RoleSystem.Admin); break;
                case "4": ApplyAsAdmin(); break;
                case "5": LogIn(RoleSystem.Moderator); break;
                case "6": return;
                default: UI.Error("Invalid choice."); break;
            }
        }

        void ShowMenu(string title, params string[] options)
        {
            Console.WriteLine();
            UI.Title(title);
            for (var i = 0; i < options.Length; i++)
                UI.Option($"{i + 1}. {options[i]}");
        }

        void LogIn(RoleSystem role)
        {
            var person = auth.LogIn(UI.Ask("Email:"), UI.Ask("Password:"), role);
            if (person is null)
            {
                UI.Error("Wrong email or password.");
                return;
            }

            UI.Success($"Welcome, {person.Name}!");
            switch (person)
            {
                case User user: RunUserMenu(user); break;
                case Admin admin: RunAdminMenu(admin); break;
                case Moderator moderator: RunModeratorMenu(moderator); break;
            }
        }

        void CreateUserAccount()
        {
            if (auth.CreateAccount(ReadAccountRequest()))
                UI.Success("Account created. You can log in now.");
            else
                UI.Error("Invalid details or email already in use.");
        }

        void ApplyAsAdmin()
        {
            if (moderators.SubmitApplication(ReadAccountRequest()))
                UI.Success("Application sent. A moderator will review it.");
            else
                UI.Error("Invalid details, email already in use, or application already pending.");
        }

        CreatingAccountRequest ReadAccountRequest()
        {
            UI.Info("Password must be 8 to 15 characters.");
            return new CreatingAccountRequest
            {
                Name = UI.Ask("Name:"),
                LastName = UI.Ask("Last name:"),
                Email = UI.Ask("Email:"),
                Password = UI.Ask("Password:")
            };
        }

        void RunUserMenu(User user)
        {
            while (true)
            {
                ShowMenu($"=== User: {user.Name} ===",
                    "Submit a request",
                    "My requests",
                    "Accept a quote",
                    "Log out");

                switch (UI.Ask("Choose:"))
                {
                    case "1": SubmitRequest(user); break;
                    case "2": ShowRequests(users.GetMyRequests(user.Id)); break;
                    case "3":
                        Report(users.AcceptQuote(AskNumber("Request id to accept:"), user.Id),
                            "Quote accepted.", "No priced request with that id."); break;
                    case "4": return;
                    default: UI.Error("Invalid choice."); break;
                }
            }
        }

        void RunAdminMenu(Admin admin)
        {
            while (true)
            {
                ShowMenu($"=== Admin: {admin.Name} ===",
                    "Pending requests",
                    "Price a request",
                    "Log out");

                switch (UI.Ask("Choose:"))
                {
                    case "1": ShowRequests(admins.GetPendingRequests()); break;
                    case "2": PriceRequest(); break;
                    case "3": return;
                    default: UI.Error("Invalid choice."); break;
                }
            }
        }

        void RunModeratorMenu(Moderator moderator)
        {
            while (true)
            {
                ShowMenu($"=== Moderator: {moderator.Name} ===",
                    "Hire an admin",
                    "List admins",
                    "Remove an admin",
                    "Pending applications",
                    "Review an application",
                    "Log out");

                switch (UI.Ask("Choose:"))
                {
                    case "1":
                        Report(moderators.HireAdmin(ReadAccountRequest()),
                            "Admin hired.", "Invalid details or email already in use."); break;
                    case "2":
                        ShowList(moderators.GetAdmins(),
                            a => $"#{a.Id} {a.Name} {a.LastName} - {a.Email}"); break;
                    case "3":
                        Report(moderators.RemoveAdmin(AskNumber("Admin id:")),
                            "Admin removed.", "No admin with that id."); break;
                    case "4":
                        ShowList(moderators.GetPendingApplications(),
                            a => $"#{a.Id} {a.Name} {a.LastName} - {a.Email}"); break;
                    case "5": ReviewApplication(); break;
                    case "6": return;
                    default: UI.Error("Invalid choice."); break;
                }
            }
        }

        void SubmitRequest(User user)
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
            var request =
                users.SubmitRequest(user.Id, new UserRequest { Category = category.Value, Details = details });
            UI.Success($"Request #{request.Id} sent. Status: {request.Status}.");
        }

        ServiceCategory? ChooseCategory()
        {
            foreach (var (category, service) in QuestionsService.All)
                UI.Option($"{(int)category}. {service.Title}");

            return int.TryParse(UI.Ask("Choose a service:"), out var number) && Enum.IsDefined((ServiceCategory)number)
                ? (ServiceCategory)number
                : null;
        }

        void PriceRequest()
        {
            var id = AskNumber("Request id:");
            var price = decimal.TryParse(UI.Ask("Enter estimated price ($):"), out var parsed) ? parsed : 0;
            var notes = UI.Ask("Enter technician notes/instructions for the user:");
            Report(admins.PriceRequest(id, price, notes),
                "Request priced.", "Request not found, not pending, or price invalid.");
        }

        void ReviewApplication()
        {
            var id = AskNumber("Application id:");
            var answer = UI.Ask("Approve (y) or reject (n)?").ToLower();
            if (answer is not ("y" or "n"))
            {
                UI.Error("Type y or n.");
                return;
            }

            var approve = answer == "y";
            Report(moderators.ReviewApplication(id, approve),
                approve ? "Admin approved and created." : "Application rejected.",
                "No pending application with that id, or email already registered.");
        }

        void ShowRequests(List<UserRequest> requests)
        {
            if (requests.Count == 0)
                UI.Info("No requests.");

            foreach (var request in requests)
            {
                UI.Title($"#{request.Id} {QuestionsService.All[request.Category].Title} - {request.Status}");
                UI.Info(request.Details);
                if (request.Price is not null)
                    UI.Info($"Price: ${request.Price}   Notes: {request.TechnicianNotes}");
            }
        }

        void ShowList<T>(List<T> items, Func<T, string> describe)
        {
            if (items.Count == 0)
                UI.Info("Nothing to show.");

            foreach (var item in items)
                UI.Info(describe(item));
        }

        void Report(bool succeeded, string success, string failure)
        {
            if (succeeded)
                UI.Success(success);
            else
                UI.Error(failure);
        }

        int AskNumber(string question) => int.TryParse(UI.Ask(question), out var number) ? number : 0;
    }
}