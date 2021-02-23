using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Commands.CreateFuelProduct
{
    public class CreateFuelProductCommandValidator : AbstractValidator<CreateFuelProductCommand>
    {
        public readonly IApplicationDbContext _context;

        public CreateFuelProductCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Fuel Product Name is required.")
                .MaximumLength(200).WithMessage("Fuel Product Name should not be more than 200.")
                .MustAsync(BeUniqueName).WithMessage("Fuel Product Name must be Unique.");

            RuleFor(v => v.Grade)
                .NotEmpty().WithMessage("Fuel Grade is required.");

            RuleFor(v => v.uom)
                .NotEmpty().WithMessage("UOM is required.");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.FuelProducts.AllAsync(l => l.Name == name, cancellationToken);
        }
    }
}
