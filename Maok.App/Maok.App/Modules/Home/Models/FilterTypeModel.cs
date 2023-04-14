using Maok.App.Modules.Home.Enums;

namespace Maok.App.Modules.Home.Models
{
    public class FilterTypeModel
    {
        public EventType EventType { get; set; }
        public bool? EventSelected { get; set; }
    }
}