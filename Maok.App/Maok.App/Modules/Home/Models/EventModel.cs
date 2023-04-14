using Maok.App.Modules.Home.Services.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maok.App.Modules.Home.Models
{
    public class EventModel
    {
        public EventModel()
        {
        }

        public EventModel(EventResponseDto dto)
        {
            Content = dto.Content.Select(eventModel => new ContentModel(eventModel)).ToList();
            Pageable = new PageableModel(dto.Pageable);
            Last = dto.Last;
            TotalElements = dto.TotalElements;
            TotalPages = dto.TotalPages;
            Sort = new SortModel(dto.Sort);
            First = dto.First;
            Number = dto.Number;
            NumberOfElements = dto.NumberOfElements;
            Size = dto.Size;
            Empty = dto.Empty;
        }

        public List<ContentModel> Content { get; set; }
        public PageableModel Pageable { get; set; }
        public bool Last { get; set; }
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
        public SortModel Sort { get; set; }
        public bool First { get; set; }
        public int Number { get; set; }
        public int NumberOfElements { get; set; }
        public int Size { get; set; }
        public bool Empty { get; set; }
    }

    public class ContentModel
    {
        public ContentModel()
        {
        }

        public ContentModel(ContentResponseDto dto)
        {
            Number = dto.Number;
            Start = dto.Start;
            IsPublic = dto.IsPublic;
            ImageUrl = dto.ImageUrl;
            PresenceConfirmed = dto.PresenceConfirmed;
            PresenceApproved = dto.PresenceApproved == null ? false : dto.PresenceApproved;
            Highlight = dto.Highlight;
            Street = dto.AddressName != null ? dto.AddressName : dto.Street != null ? (dto.Street + ", " + dto.Number) : null;
            Name = dto.Name;
            Id = dto.Id;
        }

        public string Number { get; set; }
        public string Start { get; set; }
        public bool IsPublic { get; set; }
        public string AddressName { get; set; }
        public string ImageUrl { get; set; }
        public bool? PresenceConfirmed { get; set; }
        public bool? PresenceApproved { get; set; }
        public int? Highlight { get; set; }
        public string Street { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class PageableModel
    {
        public PageableModel()
        {
        }

        public PageableModel(PageableResponseDto dto)
        {
            Sort = new SortModel(dto.Sort);
            PageNumber = dto.PageNumber;
            PageSize = dto.PageSize;
            Offset = dto.Offset;
            Paged = dto.Paged;
            Unpaged = dto.Unpaged;
        }

        public SortModel Sort { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Offset { get; set; }
        public bool Paged { get; set; }
        public bool Unpaged { get; set; }
    }

    public class SortModel
    {
        public SortModel()
        {
        }

        public SortModel(SortResponseDto dto)
        {
            Sorted = dto.Sorted;
            Unsorted = dto.Unsorted;
            Empty = dto.Empty;
        }

        public bool Sorted { get; set; }
        public bool Unsorted { get; set; }
        public bool Empty { get; set; }
    }
}