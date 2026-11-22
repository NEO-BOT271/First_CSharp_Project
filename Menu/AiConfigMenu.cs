using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Services;

namespace Project_IT_STEP.Menu;

internal class AiConfigMenu(IProviderService providers)
{
    public void Run()
    {
        while (true)
        {
            UI.ShowMenu([Texts.AppName, Texts.Moderator, Texts.AiConfig],
                Texts.CheckModels, Texts.AddProviderWithKey, Texts.AddProviderWithoutKey,
                Texts.DeleteProvider, Texts.ChooseModel, Texts.ChooseActiveProvider, Texts.Back);

            switch (UI.Ask(Texts.Choose))
            {
                case "1": ShowProviders(); UI.Pause(); break;
                case "2": AddProvider(hasKey: true); break;
                case "3": AddProvider(hasKey: false); break;
                case "4": DeleteProvider(); break;
                case "5": ChooseModel(); break;
                case "6": ChooseActiveProvider(); break;
                case "7": return;
                default: UI.Error(Texts.InvalidChoice); break;
            }
        }
    }

    private void ShowProviders()
    {
        var list = providers.GetProviders();
        if (list.Count == 0)
        {
            UI.Info(Texts.NoProviders);
            return;
        }

        foreach (var p in list)
            UI.Info($"#{p.Id}  {p.Name}  ({(p.IsLocal ? "local" : "cloud")})  {p.Url}  {(p.IsActive ? "[active]" : "")}  {p.Model}");
    }

    private void AddProvider(bool hasKey)
    {
        var name = UI.Ask(Texts.ProviderName);
        var typeChoice = UI.Ask(Texts.ProviderTypeQuestion);
        var type = typeChoice == "1" ? ProviderType.Gemini : ProviderType.OpenAiCompatible;
        var url = UI.Ask(Texts.ProviderUrl);
        var isLocal = UI.Ask(Texts.ProviderIsLocal).ToLower() == "y";
        var apiKey = hasKey ? UI.Ask(Texts.ProviderApiKey) : "";

        UI.Report(providers.AddProvider(name, type, url, apiKey, isLocal),
            Texts.ProviderAdded, Texts.ProviderFailed);
    }

    private void DeleteProvider()
    {
        ShowProviders();
        var id = UI.AskNumber(Texts.ProviderId);
        UI.Report(providers.DeleteProvider(id), Texts.ProviderDeleted, Texts.ProviderNotFound);
    }
    
    
    private void ChooseModel()
    {
        ShowProviders();
        var id = UI.AskNumber(Texts.ProviderId);
        var models = providers.GetModels(id);
        if (models.Count == 0)
        {
            UI.Error(Texts.NoModels);
            return;
        }

        for (var i = 0; i < models.Count; i++)
            UI.Option($"{i + 1}. {models[i]}");

        var choice = UI.AskNumber(Texts.ChooseModelNumber);
        if (choice < 1 || choice > models.Count)
        {
            UI.Error(Texts.InvalidChoice);
            return;
        }

        UI.Report(providers.SetModel(id, models[choice - 1]), Texts.ModelSet, Texts.ProviderNotFound);
    }

    private void ChooseActiveProvider()
    {
        ShowProviders();
        var id = UI.AskNumber(Texts.ProviderId);
        UI.Report(providers.SetActive(id), Texts.ActiveSet, Texts.ProviderNotFound);
    }
    
}