using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Commands.UpdateProduct
{

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public readonly IApplicationDbContext _context;

        public UpdateProductCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Product Name is required.")
                .MaximumLength(200).WithMessage("Fuel Product Name should not be more than 200.");

            RuleFor(v => v.uom)
                .NotEmpty().WithMessage("UOM is required.");

            RuleFor(v => v)
                .MustAsync(BeUniqueName).WithMessage("UOM is required.");

            RuleFor(v => v)
                .MustAsync(BeUniqueName).WithMessage("Fuel Product must be unique.");
        }

        public async Task<bool> BeUniqueName(UpdateProductCommand cmd, CancellationToken cancellationToken)
        {
            return await _context.Products.AllAsync(l => l.Name == cmd.Name && l.Id != cmd.Id, cancellationToken);
        }
    }
}
