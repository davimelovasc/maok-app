using Newtonsoft.Json;

namespace Maok.App.Modules.Login.Services.Dtos.Response
{
    public class LoginResponseDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("token")]
        public string token { get; set; }
    }
}