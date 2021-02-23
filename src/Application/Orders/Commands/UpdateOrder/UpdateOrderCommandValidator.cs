using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOrderCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(o => o.OrderDate).NotEmpty().WithMessage("Order Date is required");

            RuleFor(o => o.ShipDate).NotEmpty().WithMessage("Shipment Date is required");

            RuleForEach(o => o.OrderLines).NotEmpty().SetValidator(new UpdateOrderItemValidator(_context));
        }

    }


    public class UpdateOrderItemValidator : AbstractValidator<UpdateOrderLine>
    {
        private readonly IApplicationDbContext _context;
        public UpdateOrderItemValidator(IApplicationDbContext context)
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

            RuleFor(o => o.Quantituy).GreaterThan(0.0).WithMessage("Quantity must be greater than Zero");


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
