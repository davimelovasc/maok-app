using Maok.App.Modules.Home.Models.Object;
using Maok.App.Modules.Shared.Models;

namespace Maok.App.Modules.Home.Models
{
    public class EventInfoModel : BaseModel
    {
        public string Id { get; set; }
        public bool Sent { get; set; } = false;
        public bool PresenceApproved { get; set; } = false;
        public bool IsEnabled { get; set; } = true;
        public decimal Opacity { get; set; } = 1;
        public string ButtonText { get; set; } = "CONFIRMAR PRESENÇA";
        public string ButtonColor { get; set; } = "#5D25C8";
        public EventInformationModel EventInformation { get; set; }
    }
}