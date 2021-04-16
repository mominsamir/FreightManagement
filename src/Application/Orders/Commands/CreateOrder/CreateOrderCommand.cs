using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<long>
    {
        public CreateOrderCommand(long customerId, DateTime orderDate, DateTime shipDate, List<CreateOrderLine> orderLines)
        {
            CustomerId = customerId;
            OrderDate = orderDate;
            ShipDate = shipDate;
            OrderLines = orderLines;
        }

        public long CustomerId { get; }
        public DateTime OrderDate { get; }
        public DateTime ShipDate { get; }
        public List<CreateOrderLine> OrderLines { get; }

    }

    public class CreateOrderLine
    {
        public CreateOrderLine(long locationId, long fuelProductId, double quantity, string loadCode)
        {
            LocationId = locationId;
            FuelProductId = fuelProductId;
            Quantity = quantity;
            LoadCode = loadCode;
        }

        public long LocationId { get; }
        public long FuelProductId { get; }
        public double Quantity { get; }
        public string LoadCode { get; }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, long>
    {
        private readonly IApplicationDbContext _context;
        
        public CreateOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Where(c => c.Id == request.CustomerId).SingleOrDefaultAsync(cancellationToken);

            var order = new Order
            {
                Customer = customer,
                OrderDate = request.OrderDate,
                ShipDate = request.ShipDate,
            };

            var productsIds =  request.OrderLines.Select(l => l.FuelProductId).Distinct().ToList();
            var locationIds = request.OrderLines.Select(l => l.LocationId).Distinct().ToList();
            
            var fuelProducts = await _context.FuelProducts.Where(f => productsIds.Contains( f.Id)).ToListAsync(cancellationToken);
            var locations = await _context.Locations.Where(f => locationIds.Contains( f.Id)).ToListAsync(cancellationToken);

            request.OrderLines.ForEach(l =>
            {
                var fp = fuelProducts.Where(f => f.Id == l.FuelProductId).FirstOrDefault();
                var loc = locations.Where(lc => lc.Id == l.LocationId).FirstOrDefault();
                order.AddOrderItem(fp, loc, l.Quantity, l.LoadCode);
            });

            await _context.Orders.AddAsync(order, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }


}
