using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Products;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreightManagement.Domain.ValueObjects;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FreightManagement.Application.Customers.Commands.UpdateLocation
{
    public class UpdateLocationCommand : IRequest { 
       public UpdateLocationCommand(
            long id,
            string name,
            string email,
            string street,
            string city,
            string state,
            string country,
            string zipCode,
            IEnumerable<LocationTanksDto> tanks
            )
        {
            Id = id;
            Name = name;
            Email = email;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            Tanks = tanks;
        }

        public long Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }
        public IEnumerable<LocationTanksDto> Tanks { get; }

    }

    public class LocationTanksDto
    {
        public LocationTanksDto(long id, string name, FuelGrade fuelGrade, double capactity)
        {
            Id = id;
            Name = name;
            FuelGrade = fuelGrade;
            Capactity = capactity;
        }

        public long Id { get; }
        public string Name { get; }
        public FuelGrade FuelGrade { get; }
        public double Capactity { get; }
    }

    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand>
     {
        private readonly IApplicationDbContext _context;

        public UpdateLocationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _context.Locations.Include(l=> l.Tanks)
                .Where(l=> l.Id == request.Id ).SingleOrDefaultAsync(cancellationToken);

            if (location == null)
                throw new NotFoundException($"Location not found with id {request.Id }");

            location.Name = request.Name;
            location.Email = new Email(request.Email);
            location.DeliveryAddress = new Address(
                request.Street, 
                request.City, 
                request.State, 
                request.Country, 
                request.ZipCode
            );

            request.Tanks.ToList().ForEach(t =>{
                if(t.Id == 0)
                    location.AddNewTank(t.Name,t.FuelGrade, t.Capactity);
                else
                    location.UpdateTank(t.Id, t.FuelGrade, t.Capactity, t.Name);
            });

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
     }
}
