using Maok.App.Utils.Configurations;
using System.Text.Json.Serialization;

namespace Maok.App.Modules.Shared.Enums
{
    [JsonConverter(typeof(PreventStringEnumConverter))]
    public enum AlertType
    {
        Info,
        Warning,
        Error
    }
}