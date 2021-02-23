using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Orders;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest
    {
        public long Id;
        public OrderStatus Status;
    }

    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOrderStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(request.Id, cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(string.Format("Order not found with id {0}", request.Id));
            }

            switch (request.Status)
            {
                case OrderStatus.Cancelled:
                    order.MarkCancelled();
                    break;
                case OrderStatus.Delivered:
                    order.MarkDelivered();
                    break;
                case OrderStatus.Shipped:
                    order.MarkShipped();
                    break;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
