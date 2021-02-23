using FluentValidation;
using FluentValidation.Results;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Orders;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {

        private readonly IApplicationDbContext _context;

        public UpdateOrderStatusCommandValidator(IApplicationDbContext context)
        {
            _context = context;
        }

        public override async Task<ValidationResult> ValidateAsync(ValidationContext<UpdateOrderStatusCommand> entity, CancellationToken cancellation = default)
        {
            var currentOrder = await _context.Orders.FindAsync(entity.InstanceToValidate.Id, cancellation);

            RuleFor(order => order)
                .Must((order , status) => order.Status == OrderStatus.Cancelled && currentOrder.Status == OrderStatus.Delivered ? false: true)
                .WithMessage("Order is already delivered, can not cancel it.");

            RuleFor(order => order)
                .Must((order, status) => order.Status == OrderStatus.Shipped && currentOrder.Status == OrderStatus.Delivered ? false : true)
                .WithMessage("Order is already delivered, can not reship it.");


            RuleFor(order => order)
                .Must((order, status) => order.Status == OrderStatus.Delivered && currentOrder.Status == OrderStatus.Cancelled ? false : true)
                .WithMessage("Order is already cancelled, can not deliver it.");

            RuleFor(order => order)
                .Must((order, status) => order.Status == OrderStatus.Received)
                .WithMessage("Order status received cannot be set.");

            return await base.ValidateAsync(entity, cancellation);
        }


    }
}
