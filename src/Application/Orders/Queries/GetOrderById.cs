using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Queries
{
    public class GetOrderById : IRequest<ModelView<OrderDto>>
    {
        public long Id;
    }

    public class GetOrderByIdHandler : IRequestHandler<GetOrderById, ModelView<OrderDto>>
    {
        public readonly IApplicationDbContext _context;
        public GetOrderByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ModelView<OrderDto>> Handle(GetOrderById request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Include(oi => oi.OrderItems)
                .Where(o => o.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

            if(order == null)
            {
                throw new NotFoundException(string.Format("Order Not found with uid {0}", request.Id));
            }

            var orderDto = new OrderDto
            (
                order.Id,
                order.Customer,
                order.OrderDate,
                order.ShipDate,
                order.Status,
                order.OrderItems.Select(l => 
                    new OrderItemDto ( 
                        l.Id,
                        l.Location,
                        l.FuelProduct, 
                        l.Quantity,
                        l.LoadCode
                    )).ToList()
            );

           return new ModelView<OrderDto>
            {
                Model = orderDto,
                IsEditable = order.Status == OrderStatus.Received,
                IsDeletable = false,
                AddNew = true,
            };

        }
    }
}
