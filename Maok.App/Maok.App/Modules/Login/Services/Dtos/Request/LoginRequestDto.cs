using Maok.App.Modules.Login.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maok.App.Modules.Login.Services.Dtos.Request
{
    public class LoginRequestDto
    {
        public LoginRequestDto()
        {
        }

        public LoginRequestDto(LoginModel model)
        {
            Username = model.Username;
            Password = model.Password;
        }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}