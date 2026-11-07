using Project_IT_STEP.ENUM;

namespace Project_IT_STEP.Services;

public class QuestionsService
{
    public static readonly Dictionary<ServiceCategory, (string Title, string[] Questions)> All = new()
    {
        [ServiceCategory.EquipmentRepair] = ("Equipment Repair",
        [
            "Is the device a desktop, laptop, or other hardware?",
            "Describe the physical or operational symptoms (e.g., won't boot, making noise):"
        ]),
        [ServiceCategory.WifiAndNetwork] = ("Wi-Fi & Network Security",
        [
            "Are you experiencing connection drops, slow speeds, or security setup needs?",
            "Provide router model and ISP name:"
        ]),
        [ServiceCategory.HardwareInstallation] = ("Hardware Installation & Upgrades",
        [
            "Are you building a new PC or upgrading an existing one?",
            "What component(s) are you installing (e.g., GPU, RAM, SSD)?"
        ]),
        [ServiceCategory.SoftwareInstallation] = ("OS & Software Installation",
        [
            "Which operating system or software suite requires deployment?",
            "Do you require data migration or a clean installation?"
        ]),
        [ServiceCategory.RemoteSupport] = ("Remote Technical Support",
        [
            "What is the primary software or configuration issue?",
            "Are you able to install a remote-access agent (e.g., AnyDesk/SSH)?"
        ]),
        [ServiceCategory.DataRecovery] = ("Data Recovery & Backup",
        [
            "Is data inaccessible due to accidental deletion, drive failure, or corruption?",
            "What is the estimated storage volume and file type priority?"
        ])
    };
}