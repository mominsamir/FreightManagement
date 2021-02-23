using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Orders.Commands.CreateOrderItem;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Commands.CreateOrder
{
        public class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
        {
            private readonly IApplicationDbContext _context;
            public CreateOrderItemCommandValidator(IApplicationDbContext context)
            {
                _context = context;

                RuleFor(o => o.FuelProductId)
                    .NotEmpty().WithMessage("Product is required.")
                    .MustAsync(CheckIfProductExist)
                    .WithMessage(o => string.Format("Product Id {0} does not exists.", o.FuelProductId));

                RuleFor(o => o.LocationId)
                    .NotEmpty().WithMessage("Delivery Location is required.")
                    .MustAsync(CheckIfLocationExist)
                    .WithMessage(o => string.Format("Location Id {0} does not exists", o.FuelProductId));

                RuleFor(o => o.Quantituy).GreaterThan(0.0).WithMessage("Quantity must be greater than Zero.");


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
