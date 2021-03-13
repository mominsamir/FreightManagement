using AutoMapper;
using FreightManagement.Application.Common.Mappings;
using FreightManagement.Domain.Entities.Vendors;
using FreightManagement.Domain.ValueObjects;


namespace FreightManagement.Application.Vendors.Queries.GetVendor
{
    public class VendorDto : IMapFrom<Vendor>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }

        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vendor, VendorDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
        }
    }
}
