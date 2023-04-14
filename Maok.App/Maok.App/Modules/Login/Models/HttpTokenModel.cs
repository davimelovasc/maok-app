using Maok.App.Modules.Shared.Services.Dtos.Response;

namespace Maok.App.Modules.Login.Models
{
    public class HttpTokenModel
    {
        public HttpTokenModel()
        {
        }

        public HttpTokenModel(LoginTokenResponseDto dto)
        {
            Type = dto.Type;
            Token = dto.Token;
        }

        public string Type { get; set; }
        public string Token { get; set; }
    }
}