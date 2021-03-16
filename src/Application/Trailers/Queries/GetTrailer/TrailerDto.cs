using AutoMapper;
using FreightManagement.Application.Common.Mappings;
using FreightManagement.Domain.Entities.Vehicles;


namespace FreightManagement.Application.Trailers.Queries.GetTrailer
{
    public class TrailerDto : IMapFrom<Trailer>
    {

        public TrailerDto()
        {
        }

        public TrailerDto(long id, string numberPlate, string vIN, int compartment, double capacity, VehicleStatus status): this()
        {
            Id = id;
            NumberPlate = numberPlate;
            VIN = vIN;
            Compartment = compartment;
            Capacity = capacity;
            Status = status;
        }

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

    public class TrailerListDto 
    {
        public TrailerListDto(long id, string numberPlate, string vIN, double capacity, int compartment, string status)
        {
            Id = id;
            NumberPlate = numberPlate;
            VIN = vIN;
            Capacity = capacity;
            Compartment = compartment;
            Status = status;
        }

        public long Id { get; }
        public string NumberPlate { get; }
        public string VIN { get;  }
        public double Capacity { get;  }
        public int Compartment { get; }
        public string Status { get; }

    }

}
