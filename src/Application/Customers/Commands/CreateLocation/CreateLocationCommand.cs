using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.Entities.Products;
using FreightManagement.Domain.ValueObjects;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.CreateLocation
{
    public class CreateLocationCommand : IRequest<long>
    {
        public CreateLocationCommand(string name,
            string email,
            string street,
            string city,
            string state,
            string country,
            string zipCode,
            IEnumerable<LocationTanksDto> tanks

        )
        {
            Name = name;
            Email = email;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            Tanks = tanks;
        }

        public string Name { get; }
        public string Email { get; }
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }
        public IEnumerable<LocationTanksDto> Tanks {get;}

    }

    public class LocationTanksDto
    {
        public LocationTanksDto(string name, FuelGrade fuelGrade, double capactity)
        {
            Name = name;
            FuelGrade = fuelGrade;
            Capactity = capactity;
        }

        public string Name { get;  }
        public FuelGrade FuelGrade { get;  }
        public double Capactity { get; }
    }

    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, long>
    {
        private readonly IApplicationDbContext _contex;
        
        public CreateLocationCommandHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<long> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {

            var location = new Location
            {
                Name = request.Name,
                Email = new Email(request.Name),
                DeliveryAddress = new Address(
                             request.Street,
                             request.City,
                             request.State,
                             request.Country,
                             request.ZipCode
                         )
                
            };

            request.Tanks.ToList().ForEach(s => {
                location.AddNewTank(s.Name,  s.FuelGrade, s.Capactity);
            });

            await _contex.Locations.AddAsync(location,cancellationToken);
            await _contex.SaveChangesAsync(cancellationToken);
            return location.Id;
        }
    }

}
