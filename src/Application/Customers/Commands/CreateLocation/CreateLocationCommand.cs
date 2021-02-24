using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.CreateLocation
{
    public class CreateLocationCommand : IRequest<long>
    {
        public string Name;
        public string Email; 
        public string Street;
        public string City;
        public string State;
        public string Country;
        public string ZipCode;

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

            await _contex.Locations.AddAsync(location,cancellationToken);
            await _contex.SaveChangesAsync(cancellationToken);
            return location.Id;
        }
    }

}
