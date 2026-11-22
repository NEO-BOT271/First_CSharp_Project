using Project_IT_STEP.ENUM;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Interfaces;

internal interface IProviderService
{
    List<AiProvider> GetProviders();
    bool AddProvider(string name, ProviderType type, string url, string apiKey, bool isLocal);
    bool DeleteProvider(int providerId);
    List<string> GetModels(int providerId);
    bool SetModel(int providerId, string model);
    bool SetActive(int providerId);
    AiProvider? GetActive();
}