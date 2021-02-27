using AutoMapper;
using FreightManagement.Application.Common.Mappings;
using FreightManagement.Domain.Entities.Vehicles;
using System;

namespace FreightManagement.Application.Trucks.Queries
{
    public class TruckDto : IMapFrom<Truck>
    {
        public long Id { get; set; }
        public string NumberPlate { get; set; }
        public string VIN { get; set; }
        public DateTime NextMaintanceDate { get; set; }
        public VehicleStatus Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Truck, TruckDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
        }
    }
}
