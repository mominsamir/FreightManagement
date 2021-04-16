using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.RemoveLocationCustomer
{
    public class RemoveLocationFromCustomerCommand : IRequest
    {
        public RemoveLocationFromCustomerCommand(long id, long locationId)
        {
            Id = id;
            LocationId = locationId;
        }

        public long Id { get; }
        public long LocationId { get; }
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
            var customer = await _context.Customers.FindAsync(new object[] { request.Id }, cancellationToken);
            if (customer == null)
            {
                throw new NotFoundException(string.Format("Customer with Id {0} not found", request.Id));
            }

            customer.RemoveLocation(request.LocationId);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
