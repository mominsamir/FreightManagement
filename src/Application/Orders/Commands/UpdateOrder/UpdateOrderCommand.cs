using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public long Id;
        public DateTime OrderDate;
        public DateTime ShipDate;
        public List<UpdateOrderLine> OrderLines = new List<UpdateOrderLine>();
    }

    public class UpdateOrderLine
    {
        public long Id;
        public long LocationId;
        public long FuelProductId;
        public double Quantituy = 0;
        public string LoadCode;
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(new object[] { request.Id },cancellationToken);

            if(order == null)
            {
                throw new NotFoundException(string.Format("Order not found with id {0}",request.Id));
            }

            order.OrderDate = request.OrderDate;
            order.ShipDate = request.ShipDate;

            request.OrderLines.ForEach(async l =>
            {
                var fuelProduct = await _context.FuelProducts.FindAsync(l.FuelProductId, cancellationToken);
                var location = await _context.Locations.FindAsync(l.LocationId, cancellationToken);

                order.UpdateOrderItem(l.Id,fuelProduct, location, l.Quantituy,l.LoadCode);
            });

            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;

        }
    }
}
