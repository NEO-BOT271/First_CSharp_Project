using Project_IT_STEP.ENUM;

namespace Project_IT_STEP.Services;

internal static class Texts
{
    public static string Pick(string georgian, string english) =>
        UI.CurrentLanguage == Language.Georgian ? georgian : english;

    public static string StatusName(UserRequestStatus status) => status switch
    {
        UserRequestStatus.Pending => Pick("მოლოდინში", "Pending"),
        UserRequestStatus.Priced => Pick("შეფასებულია", "Priced"),
        UserRequestStatus.Accepted => Pick("მიღებულია", "Accepted"),
        _ => status.ToString()
    };

    public static string MainTitle => Pick("=== IT მხარდაჭერა ===", "=== IT SUPPORT ===");
    public static string LogInAsUser => Pick("მომხმარებლით შესვლა", "Log in as user Menu");
    public static string LogInAsAdmin => Pick("ადმინით შესვლა", "Log in as admin Menu");
    public static string LogInAsModerator => Pick("მოდერატორით შესვლა", "Log in as moderator");
    public static string Exit => Pick("გასვლა", "Exit");

    public static string Choose => Pick("აირჩიე:", "Choose:");
    public static string ChooseService => Pick("აირჩიე სერვისი:", "Choose a service:");

    public static string Email => Pick("ელფოსტა:", "Email:");
    public static string Password => Pick("პაროლი:", "Password:");
    public static string Name => Pick("სახელი:", "Name:");
    public static string User => Pick("მომხმარებელი:", "User");
    public static string Admin => Pick("ადმინისტრატორი:", "Admin");
    public static string AdminId => Pick("ადმინისტრატორის ID:", "Admin id:");

    public static string Moderator => Pick("მოდერატორი", "Moderator");

    public static string UserMenu => Pick("მომხმარებელის მენიუ:", "User Menu");
    public static string AdminMenu => Pick("ადმინისტრატორის მენიუ:", "Admin Menu");
    public static string HireAdmin => Pick("ადმინისტრატორის დამატება", "Hire an admin");
    public static string ListAdmin => Pick("ადმინისტრატორების სია", "List admins");
    public static string RemoveAdmin => Pick("ადმინისტრატორის წაშლა", "Remove an admin");
    public static string AdminRemoved => Pick("ადმინისტრატორი წაიშალა.", "Admin removed.");
    public static string AdminHired => Pick("ადმინისტრატორი დამატებულია", "Admin hired.");
    public static string AdminWithNoId => Pick("ადმინისტრატორი ამ ID-ით ვერ მოიძებნა", "No admin with that id.");

    public static string AdminApproved =>
        Pick("ადმინისტრატორი დამტკიცებულია და შექმნილია.", "Admin approved and created.");

    public static string LastName => Pick("გვარი:", "Last name:");
    public static string LogOut => Pick("გასვლა", "Log out");
    public static string Back => Pick("უკან", "Back");
    public static string LogIn => Pick("შესვლა", "Log in");
    public static string CreateAccount => Pick("ანგარიშის შექმნა", "Create account");
    public static string InvalidChoice => Pick("არასწორი არჩევანი.", "Invalid choice.");
    public static string InvalidService => Pick("არასწორი სერვისი", "Invalid service.");
    public static string Request => Pick("მოთხოვნა", "Request");
    public static string Sent => Pick("გაიგზავნა", "sent");
    public static string Status => Pick("სტატუსი:", "Status:");

    public static string WrongLogin => Pick("ელფოსტა ან პაროლი არასწორია.", "Wrong email or password.");
    public static string Welcome(string name) => Pick($"მოგესალმებით, {name}!", $"Welcome, {name}!");

    public static string AccountCreated => Pick("ანგარიში შეიქმნა. ახლა შეგიძლიათ სისტემაში შესვლა.",
        "Account created. You can log in now.");

    public static string ApplicationId => Pick("განაცხადის ID:", "Application id:");
    public static string ApplicationReject => Pick("განაცხადი უარყოფილია.", "Application rejected.");

    public static string ApplicationNoPending =>
        Pick("მითითებული ID-ით მოლოდინში მყოფი განაცხადი არ მოიძებნა, ან ელფოსტა უკვე რეგისტრირებულია.",
            "No pending application with that id, or email already registered.");

    public static string ApplicationSent => Pick("განაცხადი გაგზავნილია. მოდერატორი მას გადახედავს.",
        "Application sent. A moderator will review it.");

    public static string PendingApplication => Pick("მოლოდინში მყოფი განაცხადები", "Pending applications");
    public static string ReviewApplication => Pick("განაცხადის განხილვა", "Review an application");

    public static string InvalidDetails => Pick("არასწორი მონაცემები ან ელფოსტა უკვე გამოყენებულია.",
        "Invalid details or email already in use.");

    public static string InvalidDetailsAdmin =>
        Pick("არასწორი მონაცემები, ელფოსტა უკვე გამოყენებულია, ან განაცხადი უკვე განხილვის პროცესშია.",
            "Invalid details, email already in use, or application already pending.");

    public static string QuoteAccept => Pick("შემოთავაზებული ფასი მიღებულია.", "Quote accepted.");
    public static string AcceptQuote => Pick("შეთავაზებაზე დათანხმება", "Accept a quote");
    public static string PendingRequests => Pick("მოლოდინში მყოფი მოთხოვნები", "Pending requests");
    public static string RequestId => Pick("მოთხოვნის ID:", "Request id:");
    public static string PriceARequest => Pick("მოთხოვნის შეფასება", "Price a request");
    public static string RequestIdToAccept => Pick("მისაღები მოთხოვნის ID:", "Request id to accept:");

    public static string NoPricedRequestWithThatId =>
        Pick("ამ ID-ით ფასდადებული მოთხოვნა არ არსებობს.", "No priced request with that id.");

    public static string ReportTechnicalIssue => Pick("ტექნიკური პრობლემის დაფიქსირება", "Report technical issue");
    public static string MyRequests => Pick("ჩემი მოთხოვნები", "My requests");
    public static string RequestPriced => Pick("მოთხოვნა შეფასებულია.", "Request priced.");

    public static string RequestNotFound => Pick("მოთხოვნა ვერ მოიძებნა, არ არის მოლოდინში, ან ფასი არასწორია.",
        "Request not found, not pending, or price invalid.");

    public static string AllAnswersAreRequired =>
        Pick("ყველა კითხვაზე პასუხის გაცემა სავალდებულოა.", "All answers are required.");

    public static string EstimatedPrice => Pick("მიუთითეთ სავარაუდო ფასი (₾):", "Enter estimated price ($):");

    public static string TechnicianNotes => Pick("მიუთითეთ ტექნიკოსის კომენტარი:",
        "Enter technician notes/instructions for the user:");

    public static string PriceLabel => Pick("ფასი:", "Price:");
    public static string NotesLabel => Pick("შენიშვნები:", "Notes:");

    public static string ApproveReject => Pick("დამტკიცება (y) / უარყოფა (n)?",
        "Approve (y) or reject (n)?");

    public static string ApproveRejectElse => Pick("ჩაწერე y ან n.",
        "Type y or n.");

    public static string NoRequests => Pick("მოთხოვნები არ არის.", "No requests.");
    public static string NothingToShow => Pick("ჩანაწერები არ არის.", "Nothing to show.");

    public static string PressEnterToReturn =>
        Pick("დააჭირეთ Enter მენიუში დასაბრუნებლად...", "Press Enter to return to the menu...");

    public static string PasswordRule =>
        Pick("პაროლი უნდა იყოს 8-დან 15 სიმბოლომდე.", "Password must be 8 to 15 characters.");

    public static string AppName => Pick("IT მხარდაჭერა", "IT SUPPORT");
    public static string BackToLanguage => Pick("ენის არჩევაზე დაბრუნება", "Back to language selection");

    public static string WorksAtQuestion =>
        Pick("სად მუშაობთ ან სად მუშაობდით ადრე?", "Where do you work or did you work before?");

    public static string CertificateQuestion => Pick("გაქვთ სერტიფიკატი? (მაგ., CompTIA A+)",
        "Do you have a certificate? (e.g., CompTIA A+)");

    public static string ExperienceQuestion => Pick(
        "რა IT სფეროებში გაქვთ გამოცდილება? (მაგ., აპარატურა, ქსელი, ოპერაციული სისტემები, მონაცემთა აღდგენა)",
        "What IT areas are you experienced in? (e.g., hardware, networking, operating systems, data recovery)");
    
    public static string AiConfig => Pick("AI კონფიგურაცია", "AI Configuration");
    public static string CheckModels => Pick("მოდელების შემოწმება", "Check models");
    public static string AddProviderWithKey => Pick("პროვაიდერის დამატება API გასაღებით", "Add provider with API key");
    public static string AddProviderWithoutKey => Pick("პროვაიდერის დამატება API გასაღების გარეშე", "Add provider without API key");
    public static string ProviderName => Pick("პროვაიდერის სახელი:", "Provider name:");
    public static string ProviderUrl => Pick("URL:", "URL:");
    public static string ProviderApiKey => Pick("API გასაღები:", "API key:");
    public static string ProviderAdded => Pick("პროვაიდერი დამატებულია.", "Provider added.");
    public static string ProviderFailed => Pick("პროვაიდერი ვერ დაემატა (სახელი ან URL არასწორია, ან სახელი უკვე არსებობს).", "Provider not added (invalid name or URL, or name already exists).");
    public static string ProviderTypeQuestion => Pick("ტიპი (1 - Gemini, 2 - OpenAI-compatible):", "Type (1 - Gemini, 2 - OpenAI-compatible):");
    public static string NoProviders => Pick("პროვაიდერები არ არის დამატებული.", "No providers added.");
    public static string DeleteProvider => Pick("პროვაიდერის წაშლა", "Delete provider");
    public static string ProviderId => Pick("პროვაიდერის ID:", "Provider id:");
    public static string ProviderDeleted => Pick("პროვაიდერი წაიშალა.", "Provider deleted.");
    public static string ProviderNotFound => Pick("ამ ID-ით პროვაიდერი ვერ მოიძებნა.", "No provider with that id.");
    public static string ChooseModel => Pick("მოდელის არჩევა", "Choose model");
    public static string ChooseActiveProvider => Pick("აქტიური პროვაიდერის არჩევა", "Choose active provider");
    public static string NoModels => Pick("მოდელები ვერ მოიძებნა. შეამოწმეთ URL და API გასაღები.", "No models found. Check the URL and API key.");
    public static string ChooseModelNumber => Pick("აირჩიე მოდელის ნომერი:", "Choose model number:");
    public static string ModelSet => Pick("მოდელი დაყენდა.", "Model set.");
    public static string ActiveSet => Pick("აქტიური პროვაიდერი დაყენდა.", "Active provider set.");
    public static string ProviderIsLocal => Pick("ეს პროვაიდერი ლოკალურია? (y/n):", "Is this provider local? (y/n):");
    public static string DescribeProblem => Pick("აღწერეთ თქვენი პრობლემა ერთ წინადადებად:", "Describe your problem in one sentence:");
    public static string SuggestedService => Pick("შემოთავაზებული სერვისი:", "Suggested service:");
    public static string UseThis => Pick("გამოვიყენო? (y/n):", "Use this? (y/n):");
    
}