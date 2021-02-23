using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Commands.RemoveOrderItem
{
    public class RemoveOrderItemCommand : IRequest
    {
        public long OrderId;
        public long OrderItemId;
    }

    public class RemoveOrderItemCommandHandler : IRequestHandler<RemoveOrderItemCommand>
    {

        private readonly IApplicationDbContext _context;

        public RemoveOrderItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(string.Format("Order with id {0} not found.", request.OrderId));
            }

            order.RemoveOrderItem(request.OrderItemId);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
