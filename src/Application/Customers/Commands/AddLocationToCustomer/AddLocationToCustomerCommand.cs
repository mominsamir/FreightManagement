using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.AddLocationToCustomer
{
    public class AddLocationToCustomerCommand : IRequest 
    {
        public long customerId;
        public long locationId;
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
            var customer = await _context.Customers.FindAsync(request.customerId, cancellationToken);
            if(customer == null)
            {
                throw new NotFoundException(string.Format("Customer with Id {0} not found", request.customerId));
            }

            var location = await _context.Locations.FindAsync(request.locationId, cancellationToken);

            if (location == null)
            {
                throw new NotFoundException(string.Format("Location with Id {0} not found", request.locationId));
            }

            customer.AddLocation(location);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
