using FreightManagement.Application.Common.Mappings;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.Entities.Products;
using FreightManagement.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace FreightManagement.Application.Customers.Queries.GetLocationById
{
    public class LocationDto :IMapFrom<Location>
    {
        public long Id { get;  }
        public string Name { get; }
        public Email Email { get; }
        public bool IsActive { get;  }
        public Address DeliveryAddress { get;  }
        public IEnumerable<LocationTanksDto> Tanks { get; }

        public LocationDto(long id, string name, Email email, bool isActive, Address deliveryAddress, IEnumerable<LocationTank> tanks)
        {
            Id = id;
            Name = name;
            Email = email;
            IsActive = isActive;
            DeliveryAddress = deliveryAddress;
            Tanks = tanks.Select(t => new LocationTanksDto(t.Id, t.Name, t.FuelGrade, t.Capactity)).ToList();
        }

        public LocationDto(long id, string name, Email email, bool isActive, Address deliveryAddress)
        {
            Id = id;
            Name = name;
            Email = email;
            IsActive = isActive;
            DeliveryAddress = deliveryAddress;
        }

    }

    public class LocationTanksDto
    {
        public long Id { get; }
        public string Name { get; }
        public FuelGrade FuelGrade { get; }
        public double Capactity { get; }

        public LocationTanksDto(long id, string name, FuelGrade fuelGrade, double capactity)
        {
            Id = id;
            Name = name;
            FuelGrade = fuelGrade;
            Capactity = capactity;
        }

    }
}
