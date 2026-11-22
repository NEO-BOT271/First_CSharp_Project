# IT Support System

A console application for IT support requests with three roles: User, Admin and Moderator. Users describe a problem, an Admin prices it, and the User accepts the quote. An AI model can suggest the service category.

Built with C# on .NET 10. Data is stored in JSON files.

## Features

- **Users:** create an account, submit a request (with optional AI category suggestion), view requests, accept a price.
- **Admins:** apply for a role (approved by a Moderator), view pending requests, set a price and notes.
- **Moderators:** hire or remove admins, review admin applications, see all admins, configure AI providers and models.
- **Email notifications** via Gmail SMTP for new requests, applications, quotes and approvals.
- **Languages:** Georgian and English, chosen at startup.
- **AI suggestion** through Google Gemini or a local OpenAI-compatible server (for example Ollama). Local models keep data on your machine.

## Requirements

- .NET 10 SDK
- Optional: a Google Gemini API key, or a local server such as Ollama
- Optional: a Gmail account with an app password, for email

## Setup

1. Clone the repository and open `Project_IT_STEP.sln` in Rider or Visual Studio.
2. Build and run the project.
3. The program creates a `Data/` folder inside `bin/Debug/net10.0/` on first run.

### Email (optional)

Create `Data/emailsettings.json` in `bin/Debug/net10.0/Data/`:

```json
{
  "Host": "smtp.gmail.com",
  "Port": 587,
  "SenderEmail": "your.address@gmail.com",
  "AppPassword": "your-16-character-app-password"
}
```

Without this file the program still runs, but emails will not be sent.

### Moderator account

The first Moderator is created by hand. Add `Data/moderators.json`:

```json
[
  {
    "Id": 1,
    "Name": "Main",
    "LastName": "Moderator",
    "Email": "mod@it.com",
    "PasswordHash": "ED7CE2543FEEEE8020F50A7F3415D1C4880C4BE39CE0774D59C7DA160607EB99"
  }
]
```

This account logs in with `mod@it.com` and password `Mod12345`.

### AI (optional)

In the Moderator menu, open **AI Configuration** to add a provider:
- **Gemini:** type `1`, URL `https://generativelanguage.googleapis.com`, and your API key.
- **Local server:** type `2`, URL such as `http://localhost:11434`, and answer `y` to "local".

Then choose a model and make the provider active. Users can then get a category suggestion when submitting a request.

## Project structure

```
ENUM/          Enums (roles, statuses, service categories)
Models/        Data classes (Person, User, Admin, Moderator, requests)
Interfaces/    Service interfaces
Services/      Business logic, storage, email, AI
Menu/          Console menus for each role
UI.cs          Console helpers and colors
Program.cs     Entry point and main menu
```

## Security notes

- Passwords are hashed with SHA-256 (unsalted). Suitable for a course project, not for production.
- API keys and the email app password are stored in plain text in `Data/`. Keep the `Data/` folder out of version control.
- Keep `bin/`, `obj/`, `.idea/` and `Data/` in `.gitignore`.

## Limitations

- JSON files are not safe for many simultaneous users. A database would replace `JsonStorage<T>`.
- Gemini and local model lists include models that cannot answer text prompts.
- Language is set per run and not saved.
