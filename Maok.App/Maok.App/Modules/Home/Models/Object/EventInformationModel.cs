using Maok.App.Modules.Home.Services.Dtos.Response;
using System.Collections.Generic;
using System.Linq;

namespace Maok.App.Modules.Home.Models.Object
{
    public class EventInformationModel
    {
        public EventInformationModel()
        {
        }

        public EventInformationModel(EventInformationResponseDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Description = dto.Description;
            Start = dto.Start;
            Finish = dto.Finish;
            Status = dto.Status;
            IsPublic = dto.IsPublic.GetValueOrDefault();
            IsAutoApproveOn = dto.IsAutoApproveOn.GetValueOrDefault();
            PresenceConfirmed = dto.PresenceConfirmed.GetValueOrDefault();
            PresenceApproved = dto.PresenceApproved.GetValueOrDefault();
            Address = dto.Address != null ? new AddressModel(dto.Address) : null;
            Owner = dto.Owner != null ? new OwnerModel(dto.Owner) : null;
            Images = dto.Images != null ? dto.Images.Select(photo => new PhotoModel(photo)).ToList() : null;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
        public string Status { get; set; }
        public bool IsPublic { get; set; }
        public bool IsAutoApproveOn { get; set; }
        public bool PresenceConfirmed { get; set; }
        public bool PresenceApproved { get; set; }
        public AddressModel Address { get; set; }
        public OwnerModel Owner { get; set; }
        public List<PhotoModel> Images { get; set; }
    }

    public class AddressModel
    {
        public AddressModel()
        {
        }

        public AddressModel(AddressResponseDto dto)
        {
            Street = dto.DisplayName != null ? dto.DisplayName : (dto.Street + ", " + dto.Number);
            Number = dto.Number;
        }

        public string Street { get; set; }
        public string Number { get; set; }
        public string DisplayName { get; set; }
    }

    public class OwnerModel
    {
        public OwnerModel()
        {
        }

        public OwnerModel(OwnerResponseDto dto)
        {
            Name = dto.Name;
            PhotoUrl = dto.PhotoUrl;
        }

        public string Name { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class PhotoModel
    {
        public PhotoModel()
        {
        }

        public PhotoModel(PhotoResponseDto dto)
        {
            Seq = dto.Seq;
            Url = dto.Url;
        }

        public int Seq { get; set; }
        public string Url { get; set; }
    }
}