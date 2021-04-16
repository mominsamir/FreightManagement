using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.AddLocationToCustomer
{
    public class AddLocationToCustomerCommand : IRequest 
    {
        public AddLocationToCustomerCommand(long id, long locationId)
        {
            Id = id;
            LocationId = locationId;
        }

        public long Id { get; }
        public long LocationId { get; }
    }

    public class AddLocationToCustomerCommandHandler : IRequestHandler<AddLocationToCustomerCommand>
    {
        private readonly IApplicationDbContext _context;
        public AddLocationToCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddLocationToCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FindAsync(new object[] { request.Id }, cancellationToken);
            if(customer == null)
            {
                throw new NotFoundException(string.Format("Customer with Id {0} not found", request.Id));
            }

            var location = await _context.Locations.FindAsync(new object[] { request.LocationId }, cancellationToken);

            if (location == null)
            {
                throw new NotFoundException(string.Format("Location with Id {0} not found", request.LocationId));
            }

            customer.AddLocation(location);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
