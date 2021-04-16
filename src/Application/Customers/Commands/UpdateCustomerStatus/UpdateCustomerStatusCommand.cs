using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.UpdateCustomerStatus
{
    public class UpdateCustomerStatusCommand : IRequest
    {
        public UpdateCustomerStatusCommand(long id, bool status)
        {
            Id = id;
            Status = status;
        }

        public long Id { get; }
        public bool Status { get; }
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
            var customer = await _context.Customers.FindAsync(new object[] { request.Id }, cancellationToken);

            if (customer == null)
            {
                throw new NotFoundException(string.Format("Customer not found with id {0}", request.Id));
            }

            if (request.Status) { customer.MarkActive(); } else { customer.MarkInActive(); }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
