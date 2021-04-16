using FreightManagement.Domain.Entities.Vehicles;
using System;

namespace FreightManagement.Application.Trucks.Queries
{
    public class TruckDto 
    {

        public TruckDto(long id, string numberPlate, string vIN, DateTime nextMaintanceDate, VehicleStatus status) 
        {
            Id = id;
            NumberPlate = numberPlate;
            VIN = vIN;
            NextMaintanceDate = nextMaintanceDate;
            Status = status;
        }

        public long Id { get; set; }
        public string NumberPlate { get; set; }
        public string VIN { get; set; }
        public DateTime NextMaintanceDate { get; set; }
        public VehicleStatus Status { get; set; }


    }

    public class TruckListDto 
    {
        public TruckListDto(long id, string numberPlate, string vIN, DateTime nextMaintanceDate, string status)
        {
            Id = id;
            NumberPlate = numberPlate;
            VIN = vIN;
            NextMaintanceDate = nextMaintanceDate;
            Status = status;
        }

        public long Id { get;  }
        public string NumberPlate { get;}
        public string VIN { get;  }
        public DateTime NextMaintanceDate { get;  }
        public string Status { get; }

    }
}
