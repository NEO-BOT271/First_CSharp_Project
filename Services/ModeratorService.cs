using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Services;

public class ModeratorService : IModeratorService
{
    
    private readonly JsonStorage<Admin> Admins = new("admins.json");
    private readonly JsonStorage<AdminApplication> Applications = new("applications.json");
    private readonly EmailRegistry Emails = new();

    public bool HireAdmin(CreatingAccountRequest request)
    {
        if (!request.IsValid() || Emails.Exists(request.Email))
        {
            return false;
        }

        AddAdmin(request.Name, request.LastName, request.Email, PasswordHasher.Hash(request.Password));
        return true;
    }

    public List<Admin> GetAdmins() => Admins.Load();

    public bool RemoveAdmin(int adminId)
    {
        var admins = Admins.Load();
        if (admins.RemoveAll(a => a.Id == adminId) == 0)
            return false;

        Admins.Save(admins);
        return true;
    }

    public bool SubmitApplication(CreatingAccountRequest request)
    {
        if (!request.IsValid() || Emails.Exists(request.Email))
        {
            return false;
        }

        var applications = Applications.Load();
        if (applications.Any(a => a.Status == ApplicationStatus.Pending
                                  && string.Equals(a.Email, request.Email, StringComparison.OrdinalIgnoreCase)))
            return false;

        applications.Add(new AdminApplication
        {
            Id = applications.Count == 0 ? 1 : applications.Max(a => a.Id) + 1,
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = PasswordHasher.Hash(request.Password)
        });
        Applications.Save(applications);
        return true;
    }

    public List<AdminApplication> GetPendingApplications() =>
        Applications.Load().Where(a => a.Status == ApplicationStatus.Pending).ToList();

    public bool ReviewApplication(int applicationId, bool approve)
    {
        var applications = Applications.Load();
        var application = applications.FirstOrDefault(a =>
            a.Id == applicationId && a.Status == ApplicationStatus.Pending);

        if (application is null)
            return false;

        if (approve)
        {
            if (Emails.Exists(application.Email))
                return false;

            AddAdmin(application.Name, application.LastName, application.Email, application.PasswordHash);
        }

        application.Status = approve ? ApplicationStatus.Approved : ApplicationStatus.Rejected;
        Applications.Save(applications);
        return true;
    }

    private void AddAdmin(string name, string lastName, string email, string passwordHash)
    {
        var admins = Admins.Load();
        admins.Add(new Admin
        {
            Id = admins.Count == 0 ? 1 : admins.Max(a => a.Id) + 1,
            Name = name,
            LastName = lastName,
            Email = email,
            PasswordHash = passwordHash
        });
        Admins.Save(admins);
    }
    
}