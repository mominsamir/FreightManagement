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

            RuleFor(o => o.ShipDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Delivery Date is required")
                .GreaterThanOrEqualTo(s => s.OrderDate)
                .WithMessage("Delivery date must be on or after order date.");

            RuleForEach(o => o.OrderItems).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Atleast one line is required.")
                .SetValidator(new UpdateOrderItemValidator(_context));

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

            RuleFor(o => o.Quantity)
                .GreaterThan(0.0).WithMessage("OrderedQuantity must be greater than Zero");


        }

        public async Task<bool> CheckIfLocationExist(long locationId, CancellationToken cancellationToken)
        {
            return await _context.Locations.AnyAsync(l => l.Id == locationId, cancellationToken);
        }

        public async Task<bool> CheckIfProductExist(long productId, CancellationToken cancellationToken)
        {
            return await _context.Products.AnyAsync(l => l.Id == productId, cancellationToken);
        }
    }
}
