using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Services;

internal class AdminService : IAdminService
{
    private readonly JsonStorage<UserRequest> Storage = new("requests.json");

    public List<UserRequest> GetPendingRequests() =>
        Storage.Load()
            .Where(r => r.Status == UserRequestStatus.Pending)
            .ToList();

    public bool PriceRequest(int requestId, decimal price, string notes)
    {
        if (price <= 0)
        {
            return false;
        }

        var requests = Storage.Load();
        var request = requests.FirstOrDefault(r =>
            r.Id == requestId && r.Status
            == UserRequestStatus.Pending);

        if (request is null)
        {
            return false;
        }

        request.Price = price;
        request.TechnicianNotes = notes;
        request.Status = UserRequestStatus.Priced;
        Storage.Save(requests);
        return true;
    }
}