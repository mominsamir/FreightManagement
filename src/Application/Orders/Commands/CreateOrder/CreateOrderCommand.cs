using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<long>
    {
        public long CustomerId;
        public DateTime OrderDate;
        public DateTime ShipDate;
        public List<CreateOrderLine> OrderLines = new List<CreateOrderLine>();
    }

    // TODO: Misspelling of the word Quantity requires database update seems like
    public class CreateOrderLine
    {
        public long LocationId;
        public long FuelProductId;
        public double Quantituy= 0; // Misspelling on Quantity see below
        public string LoadCode;
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
            var customer = await _context.Customers.FindAsync(request.CustomerId, cancellationToken);

            var order = new Order
            {
                Customer = customer,
                OrderDate = request.OrderDate,
                ShipDate = request.ShipDate,
            };

            request.OrderLines.ForEach(async l =>
            {
                var fuelProduct = await _context.FuelProducts.FindAsync(l.FuelProductId, cancellationToken);
                var location = await _context.Locations.FindAsync(l.FuelProductId, cancellationToken);
                order.AddOrderItem(fuelProduct, location, l.Quantituy, l.LoadCode);
                //                                            ^   
                // I would have rename this ------------------|
                // but it requires to create a migration for the database too 
            });

            await _context.Orders.AddAsync(order, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }


}
