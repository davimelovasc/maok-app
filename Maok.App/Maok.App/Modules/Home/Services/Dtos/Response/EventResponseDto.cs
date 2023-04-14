using Newtonsoft.Json;
using System.Collections.Generic;

namespace Maok.App.Modules.Home.Services.Dtos.Response
{
    public class EventResponseDto
    {
        [JsonProperty("content")]
        public List<ContentResponseDto> Content { get; set; }

        [JsonProperty("pageable")]
        public PageableResponseDto Pageable { get; set; }

        [JsonProperty("last")]
        public bool Last { get; set; }

        [JsonProperty("totalElements")]
        public int TotalElements { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("sort")]
        public SortResponseDto Sort { get; set; }

        [JsonProperty("first")]
        public bool First { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("numberOfElements")]
        public int NumberOfElements { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("empty")]
        public bool Empty { get; set; }
    }

    public class ContentResponseDto
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("isPublic")]
        public bool IsPublic { get; set; }

        [JsonProperty("addressName")]
        public string AddressName { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("presenceConfirmed")]
        public bool? PresenceConfirmed { get; set; }

        [JsonProperty("presenceApproved")]
        public bool? PresenceApproved { get; set; }

        [JsonProperty("highlight")]
        public int? Highlight { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class PageableResponseDto
    {
        [JsonProperty("sort")]
        public SortResponseDto Sort { get; set; }

        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("paged")]
        public bool Paged { get; set; }

        [JsonProperty("unpaged")]
        public bool Unpaged { get; set; }
    }

    public class SortResponseDto
    {
        [JsonProperty("unpaged")]
        public bool Sorted { get; set; }

        [JsonProperty("unsorted")]
        public bool Unsorted { get; set; }

        [JsonProperty("empty")]
        public bool Empty { get; set; }
    }
}