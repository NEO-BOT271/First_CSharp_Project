using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Services;

internal class ProviderService : IProviderService
{
    private readonly JsonStorage<AiProvider> Providers = new("providers.json");

    public List<AiProvider> GetProviders() => Providers.Load();

    public bool AddProvider(string name, ProviderType type, string url, string apiKey, bool isLocal)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(url))
            return false;

        var providers = Providers.Load();
        if (providers.Any(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)))
            return false;

        providers.Add(new AiProvider
        {
            Id = providers.Count == 0 ? 1 : providers.Max(p => p.Id) + 1,
            Name = name.Trim(),
            Type = type,
            Url = url.Trim(),
            ApiKey = apiKey.Trim(),
            IsLocal = isLocal
        });
        Providers.Save(providers);
        return true;
    }
    public bool DeleteProvider(int providerId)
    {
        var providers = Providers.Load();
        if (providers.RemoveAll(p => p.Id == providerId) == 0)
            return false;

        Providers.Save(providers);
        return true;
    }
    
    public List<string> GetModels(int providerId)
    {
        var provider = Providers.Load().FirstOrDefault(p => p.Id == providerId);
        return provider is null ? [] : ModelFetcher.Fetch(provider);
    }

    public bool SetModel(int providerId, string model)
    {
        var providers = Providers.Load();
        var provider = providers.FirstOrDefault(p => p.Id == providerId);
        if (provider is null || string.IsNullOrWhiteSpace(model))
            return false;

        provider.Model = model;
        Providers.Save(providers);
        return true;
    }

    public bool SetActive(int providerId)
    {
        var providers = Providers.Load();
        if (providers.All(p => p.Id != providerId))
            return false;

        foreach (var p in providers)
            p.IsActive = p.Id == providerId;
        Providers.Save(providers);
        return true;
    }

    public AiProvider? GetActive() => Providers.Load().FirstOrDefault(p => p.IsActive);
}