using Project_IT_STEP.ENUM;
using Project_IT_STEP.Interfaces;
using Project_IT_STEP.Models;

namespace Project_IT_STEP.Services;

internal class ModeratorService : IModeratorService
{
    private readonly JsonStorage<Admin> Admins = new("admins.json");
    private readonly JsonStorage<AdminApplication> Applications = new("applications.json");
    private readonly EmailRegistry Emails = new();
    private readonly IEmailService Email = new EmailService();

    public bool HireAdmin(CreatingAccountRequest request)
    {
        if (!request.IsValid() || Emails.Exists(request.Email))
            return false;

        AddAdmin(request.Name, request.LastName, request.Email,
            PasswordHasher.Hash(request.Password), "", "", "");
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

    public bool SubmitApplication(CreatingAccountRequest request, string worksAt, string certificate, string experience)
    {
        if (!request.IsValid() || Emails.Exists(request.Email))
            return false;

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
            PasswordHash = PasswordHasher.Hash(request.Password),
            WorksAt = worksAt,
            Certificate = certificate,
            Experience = experience
        });
        Applications.Save(applications);

        Email.SendToModerators(EmailTemplates.ApplicationSubject,
            EmailTemplates.ApplicationReceived(request.Name, request.LastName, request.Email,
                worksAt, certificate, experience));
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

            AddAdmin(application.Name, application.LastName, application.Email, application.PasswordHash,
                application.WorksAt, application.Certificate, application.Experience);
        }

        applications.Remove(application);
        Applications.Save(applications);

        if (approve)
            Email.Send(application.Email, EmailTemplates.AdminApprovedSubject,
                EmailTemplates.AdminApproved(application.Name));
        return true;
    }

    private void AddAdmin(string name, string lastName, string email, string passwordHash,
        string worksAt, string certificate, string experience)
    {
        var admins = Admins.Load();
        admins.Add(new Admin
        {
            Id = admins.Count == 0 ? 1 : admins.Max(a => a.Id) + 1,
            Name = name,
            LastName = lastName,
            Email = email,
            PasswordHash = passwordHash,
            WorksAt = worksAt,
            Certificate = certificate,
            Experience = experience
        });
        Admins.Save(admins);
    }
}