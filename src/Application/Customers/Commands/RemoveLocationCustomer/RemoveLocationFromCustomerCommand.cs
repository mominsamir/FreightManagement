using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.RemoveLocationCustomer
{
    public class RemoveLocationFromCustomerCommand : IRequest
    {
        public long customerId;
        public long locationId;
    }

    public class RemoveLocationFromCustomerCommandHandler : IRequestHandler<RemoveLocationFromCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public RemoveLocationFromCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(RemoveLocationFromCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FindAsync(new object[] { request.customerId }, cancellationToken);
            if (customer == null)
            {
                throw new NotFoundException(string.Format("Customer with Id {0} not found", request.customerId));
            }

            customer.RemoveLocation(request.locationId);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
