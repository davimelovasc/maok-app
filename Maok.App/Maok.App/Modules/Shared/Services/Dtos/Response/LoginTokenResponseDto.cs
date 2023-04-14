using Maok.App.Modules.Shared.Services.Dtos.Base;
using Newtonsoft.Json;

namespace Maok.App.Modules.Shared.Services.Dtos.Response
{
    public class LoginTokenResponseDto : BaseResponseDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}