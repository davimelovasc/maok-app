using Newtonsoft.Json;

namespace Maok.App.Modules.Register.Services.Dtos.Response
{
    public class UserResponseDto
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("number")]
        public int? Number { get; set; }

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("complement")]
        public string Complement { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("externalId")]
        public int? ExternalId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birthdate")]
        public string Birthdate { get; set; }
    }
}