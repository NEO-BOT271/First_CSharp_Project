using Project_IT_STEP.ENUM;
using Project_IT_STEP;

namespace Project_IT_STEP.Services;

internal static class QuestionsService
{
    public static readonly Dictionary<ServiceCategory, (string Title, string[] Questions)> All = new()
    {
        [ServiceCategory.EquipmentRepair] = (
            Texts.Pick("მოწყობილობის შეკეთება", "Equipment Repair"),
            [
                Texts.Pick("რა ტიპის მოწყობილობაა: დესკტოპი, ლეპტოპი თუ სხვა?", "Is the device a desktop, laptop, or other hardware?"),
                Texts.Pick("აღწერეთ ფიზიკური ან ოპერაციული პრობლემა (მაგ., არ ჩაირთვება, ხმაური):", "Describe the physical or operational symptoms (e.g., won't boot, making noise):")
            ]),
        [ServiceCategory.WifiAndNetwork] = (
            Texts.Pick("Wi-Fi და ქსელის უსაფრთხოება", "Wi-Fi & Network Security"),
            [
                Texts.Pick("გაქვთ კავშირის გაწყვეტა, ნელი სიჩქარე, ან უსაფრთხოების დაყენების საჭიროება?", "Are you experiencing connection drops, slow speeds, or security setup needs?"),
                Texts.Pick("მიუთითეთ როუტერის მოდელი და ინტერნეტ მომწოდებელი:", "Provide router model and ISP name:")
            ]),
        [ServiceCategory.HardwareInstallation] = (
            Texts.Pick("ტექნიკის დაყენება და განახლება", "Hardware Installation & Upgrades"),
            [
                Texts.Pick("ახალ კომპიუტერს აწყობთ, თუ არსებულს აახლებთ?", "Are you building a new PC or upgrading an existing one?"),
                Texts.Pick("რა კომპონენტ(ებ)ს აყენებთ (მაგ., GPU, RAM, SSD)?", "What component(s) are you installing (e.g., GPU, RAM, SSD)?")
            ]),
        [ServiceCategory.SoftwareInstallation] = (
            Texts.Pick("ოპერაციული სისტემისა და პროგრამების დაყენება", "OS & Software Installation"),
            [
                Texts.Pick("რომელი ოპერაციული სისტემა ან პროგრამული პაკეტი უნდა დაყენდეს?", "Which operating system or software suite requires deployment?"),
                Texts.Pick("გჭირდებათ მონაცემების გადატანა, თუ სუფთა ინსტალაცია?", "Do you require data migration or a clean installation?")
            ]),
        [ServiceCategory.RemoteSupport] = (
            Texts.Pick("დისტანციური ტექნიკური მხარდაჭერა", "Remote Technical Support"),
            [
                Texts.Pick("რა არის ძირითადი პროგრამული ან კონფიგურაციული პრობლემა?", "What is the primary software or configuration issue?"),
                Texts.Pick("შეგიძლიათ დისტანციური წვდომის პროგრამის დაყენება (მაგ., AnyDesk/SSH)?", "Are you able to install a remote-access agent (e.g., AnyDesk/SSH)?")
            ]),
        [ServiceCategory.DataRecovery] = (
            Texts.Pick("მონაცემთა აღდგენა და სარეზერვო ასლი", "Data Recovery & Backup"),
            [
                Texts.Pick("მონაცემები მიუწვდომელია შემთხვევითი წაშლის, დისკის უკმარისობის ან დაზიანების გამო?", "Is data inaccessible due to accidental deletion, drive failure, or corruption?"),
                Texts.Pick("რა მოცულობის საცავია და რომელი ფაილის ტიპებია პრიორიტეტული?", "What is the estimated storage volume and file type priority?")
            ])
    };
}