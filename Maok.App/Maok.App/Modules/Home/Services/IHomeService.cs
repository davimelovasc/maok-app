using Maok.App.Modules.Home.Services.Dtos.Response;
using Maok.App.Modules.Shared.Services.Dtos.Base;
using Refit;
using System.Threading.Tasks;

namespace Maok.App.Modules.Home.Services
{
    public interface IHomeService
    {
        [Get("/events?page=0")]
        Task<EventResponseDto> GetEvents();

        [Get("/events/{id}")]
        Task<EventInformationResponseDto> GetEventById(string id);

        [Patch("/events/{id}/invite")]
        Task<EventInformationResponseDto> SendPresence(string id, [Body] bool presenceConfirm);

        [Post("/voucher/{id}")]
        Task<BaseResponseDto> SendVoucher(string id);
    }
}
