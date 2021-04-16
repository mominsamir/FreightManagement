using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public UpdateOrderCommand(long id, DateTime orderDate, DateTime shipDate, List<UpdateOrderLine> orderItems)
        {
            Id = id;
            OrderDate = orderDate;
            ShipDate = shipDate;
            OrderItems = orderItems;
        }

        public long Id { get; }
        public DateTime OrderDate { get; }
        public DateTime ShipDate { get; }
        public List<UpdateOrderLine> OrderItems { get; }
    }

    public class UpdateOrderLine
    {
        public UpdateOrderLine(long id, long locationId, long fuelProductId, double quantity, string loadCode)
        {
            Id = id;
            LocationId = locationId;
            FuelProductId = fuelProductId;
            Quantity = quantity;
            LoadCode = loadCode;
        }

        public long Id { get; }
        public long LocationId { get; }
        public long FuelProductId { get; }
        public double Quantity { get; }
        public string LoadCode { get; }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger logger;

        public UpdateOrderCommandHandler(IApplicationDbContext context, ILogger<UpdateOrderCommandHandler> logger)
        {
            _context = context;
            this.logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Include(o=> o.OrderItems)
                .Where(l=> l.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

            if(order == null)
                throw new NotFoundException($"Order not found with id {request.Id}");

            order.OrderDate = request.OrderDate;
            order.ShipDate = request.ShipDate;

            var productsIds = request.OrderItems.Select(l => l.FuelProductId).Distinct().ToList();
            var locationIds = request.OrderItems.Select(l => l.LocationId).Distinct().ToList();

            var fuelProducts = await _context.FuelProducts.Where(f => productsIds.Contains(f.Id)).ToListAsync(cancellationToken);
            var locations = await _context.Locations.Where(f => locationIds.Contains(f.Id)).ToListAsync(cancellationToken);

            logger.LogInformation($"<============> order Items count={request.OrderItems.Count()}<============>");
            request.OrderItems.ForEach(l =>
            {
                logger.LogInformation($"<============> id={l.Id}, location={l.LocationId} <============>");
                var fp = fuelProducts.Where(f => f.Id == l.FuelProductId).FirstOrDefault();
                var loc = locations.Where(lc => lc.Id == l.LocationId).FirstOrDefault();
                if (l.Id == 0)
                    order.AddOrderItem(fp, loc, l.Quantity, l.LoadCode);
                else
                    order.UpdateOrderItem(l.Id,fp, loc, l.Quantity,l.LoadCode);
            });

            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;

        }
    }
}
