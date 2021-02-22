using AutoMapper;
using FreightManagement.Application.Common.Mappings;
using FreightManagement.Domain.Entities.Vehicles;


namespace FreightManagement.Application.Trailers.Queries.GetRacks
{
    public class TrailerDto : IMapFrom<Trailer>
    {

        public long Id { get; set; }
        public string NumberPlate { get; set; }
        public string VIN { get; set; }
        public double Capacity { get; set; }
        public int Compartment { get; set; }
        public VehicleStatus Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Trailer, TrailerDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
        }
    }
}
