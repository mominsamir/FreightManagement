using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest
    {
        public UpdateOrderStatusCommand(long id, OrderStatus status)
        {
            Id = id;
            Status = status;
        }

        public long Id { get; }
        public OrderStatus Status { get; }
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

            var order = await _context.Orders.Where(l=>l.Id == request.Id)
                .SingleOrDefaultAsync( cancellationToken);

            if (order == null)
                throw new NotFoundException(string.Format("Order not found with id {0}", request.Id));

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
