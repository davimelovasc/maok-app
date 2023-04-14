using Maok.App.Modules.Register.Services.Dtos.Request;
using Maok.App.Modules.Register.Services.Dtos.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Maok.App.Modules.Register.Services
{
    public interface ICadastroService
    {
        [Get("/users/validate?username={username}")]
        Task<HttpResponseMessage> CheckUsername(string username);

        [Get("/users/validate?cpf={cpf}")]
        Task<HttpResponseMessage> CheckCpf(string cpf);

        [Post("/users")]
        Task<UserResponseDto> RegisterUser(UserRequestDto request);
    }
}