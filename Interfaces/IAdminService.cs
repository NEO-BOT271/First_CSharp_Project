using Project_IT_STEP.Models;

namespace Project_IT_STEP.Interfaces;

internal interface IAdminService
{
    List<UserRequest> GetPendingRequests();
    bool PriceRequest(int requestId, decimal price, string notes);
}