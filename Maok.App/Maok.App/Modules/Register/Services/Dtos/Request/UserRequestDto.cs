using Maok.App.Modules.Register.Models;
using Newtonsoft.Json;

namespace Maok.App.Modules.Register.Services.Dtos.Request
{
    public class UserRequestDto
    {
        public UserRequestDto()
        {
        }

        public UserRequestDto(UserModel model)
        {
            ExternalId = model.ExternalId;
            Username = model.Username;
            Password = model.Password;
            Name = model.Name;
            Cpf = model.Cpf;
            Gender = model.Gender;
            BirthDate = model.BirthDate;
        }

        [JsonProperty("externalId")]
        public int ExternalId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birthdate")]
        public string BirthDate { get; set; }
    }
}
