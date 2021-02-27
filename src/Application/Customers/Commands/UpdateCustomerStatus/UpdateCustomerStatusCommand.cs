using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.UpdateCustomerStatus
{
    public class UpdateCustomerStatusCommand : IRequest
    {
        public long customerId;
        public bool status;
    }

    public class UpdateCustomerStatusCommandHandler : IRequestHandler<UpdateCustomerStatusCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCustomerStatusCommandHandler(IApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCustomerStatusCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FindAsync(new object[] { request.customerId }, cancellationToken);

            if (customer == null)
            {
                throw new NotFoundException(string.Format("Customer not found with id {0}", request.customerId));
            }

            if (request.status) { customer.MarkActive(); } else { customer.MarkInActive(); }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
