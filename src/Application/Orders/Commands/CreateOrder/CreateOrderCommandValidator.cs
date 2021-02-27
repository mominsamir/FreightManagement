using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateOrderCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(o => o.CustomerId)
                .NotEmpty().WithMessage("Cusotmer is required")
                .MustAsync(CheckIfCustomerExist).WithMessage(o=> string.Format("Customer Id {0} does not exists", o.CustomerId));

            RuleFor(o => o.OrderDate).NotEmpty().WithMessage("Order Date is required");

            RuleFor(o => o.ShipDate).NotEmpty().WithMessage("Delivery Date is required")
                .GreaterThanOrEqualTo(s=> s.OrderDate)
                .WithMessage("Delivery date must be on or after order date.");

            RuleForEach(o => o.OrderLines).NotEmpty().SetValidator(new OrderItemValidator(_context));
        }

        public async Task<bool> CheckIfCustomerExist(long customerId, CancellationToken cancellationToken)
        {
            return await _context.Customers.AllAsync(l => l.Id == customerId, cancellationToken);
        }
    }


    public class OrderItemValidator : AbstractValidator<CreateOrderLine>
    {
        private readonly IApplicationDbContext _context;
        public OrderItemValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(o => o.FuelProductId)
                .NotEmpty().WithMessage("Product is required")
                .MustAsync(CheckIfProductExist)
                .WithMessage(o => string.Format("Product Id {0} does not exists", o.FuelProductId));

            RuleFor(o => o.LocationId)
                .NotEmpty().WithMessage("Delivery Location is required")
                .MustAsync(CheckIfLocationExist)
                .WithMessage(o => string.Format("Location Id {0} does not exists", o.FuelProductId));

            RuleFor(o => o.Quantity).GreaterThan(0.0).WithMessage("OrderedQuantity must be greater than Zero");

            //TODO: check if location belongs to this customer 

        }

        public async Task<bool> CheckIfLocationExist(long locationId, CancellationToken cancellationToken)
        {
            return await _context.Locations.AllAsync(l => l.Id == locationId, cancellationToken);
        }

        public async Task<bool> CheckIfProductExist(long productId, CancellationToken cancellationToken)
        {
            return await _context.Products.AllAsync(l => l.Id == productId, cancellationToken);
        }
    }
}
