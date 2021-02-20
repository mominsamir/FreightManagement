using AutoMapper;
using FreightManagement.Application.Common.Mappings;
using FreightManagement.Domain.Entities.StorageRack;
using FreightManagement.Domain.ValueObjects;

namespace FreightManagement.Application.StorageRacks.Queries.GetRacks
{
    public class RackDto : IMapFrom<Rack>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string IRSCode { get; set; }

        public Address Address { get; set; }

        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Rack, RackDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
        }
    }
}
