using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Commands.CreateOrderItem
{
    public class CreateOrderItemCommand : IRequest
    {
        public long orderId;
        public long LocationId;
        public long FuelProductId;
        public double Quantituy = 0;
        public string LoadCode;
    }

    public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand>
    {

        private readonly IApplicationDbContext _context;

        public CreateOrderItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(request.orderId, cancellationToken);
            
            if(order == null)
            {
                throw new NotFoundException(string.Format("Order with id {0} not found.", request.orderId));
            }

            var fuelProduct = await _context.FuelProducts.FindAsync(request.FuelProductId, cancellationToken);
            var location = await _context.Locations.FindAsync(request.LocationId, cancellationToken);

            order.AddOrderItem(fuelProduct, location, request.Quantituy, request.LoadCode);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
            
        }
    }
}
