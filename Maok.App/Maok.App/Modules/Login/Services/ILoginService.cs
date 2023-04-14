using Maok.App.Modules.Login.Services.Dtos.Request;
using Maok.App.Modules.Shared.Services.Dtos.Response;
using Refit;
using System.Threading.Tasks;

namespace Maok.App.Modules.Login.Services
{
    public interface ILoginService
    {
        [Post("/auth")]
        Task<LoginTokenResponseDto> Auth(LoginRequestDto request);
    }
}