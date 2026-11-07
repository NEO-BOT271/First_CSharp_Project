using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;
using Project_IT_STEP.Services;


namespace workingproject.Services;

internal class UserService : IUserService
{
    private readonly JsonStorage<UserRequest> Storage = new("requests.json");

    public UserRequest SubmitRequest(int userId, UserRequest request)
    {
        var requests = Storage.Load();
        request.Id = requests.Count == 0 ? 1 : requests.Max(r => r.Id) + 1;
        request.UserId = userId;
        request.Status = UserRequestStatus.Pending;
        requests.Add(request);
        Storage.Save(requests);
        return request;
    }

    public List<UserRequest> GetMyRequests(int userId) =>
        Storage.Load()
            .Where(r => r.UserId == userId)
            .ToList();


    public bool AcceptQuote(int requestId, int userId)
    {
        var requests = Storage.Load();
        var request = requests.FirstOrDefault(r => r.Id ==
            requestId && r.UserId ==
            userId && r.Status == UserRequestStatus.Priced);

        if (request is null)
        {
            return false;
        }

        request.Status = UserRequestStatus.Accepted;
        Storage.Save(requests);
        return true;
    }
}