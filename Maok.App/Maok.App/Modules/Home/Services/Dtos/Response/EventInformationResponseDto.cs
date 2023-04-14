using Newtonsoft.Json;
using System.Collections.Generic;

namespace Maok.App.Modules.Home.Services.Dtos.Response
{
    public class EventInformationResponseDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("Finish")]
        public string Finish { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("isPublic")]
        public bool? IsPublic { get; set; }

        [JsonProperty("isAutoApproveOn")]
        public bool? IsAutoApproveOn { get; set; }

        [JsonProperty("presenceConfirmed")]
        public bool? PresenceConfirmed { get; set; }

        [JsonProperty("presenceApproved")]
        public bool? PresenceApproved { get; set; }

        [JsonProperty("address")]
        public AddressResponseDto Address { get; set; }

        [JsonProperty("owner")]
        public OwnerResponseDto Owner { get; set; }

        [JsonProperty("images")]
        public List<PhotoResponseDto> Images { get; set; }
    }

    public class AddressResponseDto
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }

    public class OwnerResponseDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photoUrl")]
        public string PhotoUrl { get; set; }
    }

    public class PhotoResponseDto
    {
        [JsonProperty("seq")]
        public int Seq { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}