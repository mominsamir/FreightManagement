using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Commands.UpdateFuelProduct
{

    public class UpdateFuelProductCommandValidator : AbstractValidator<UpdateFuelProductCommand>
    {
        public readonly IApplicationDbContext _context;

        public UpdateFuelProductCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Fuel Product Name is required.")
                .MaximumLength(200).WithMessage("Fuel Product Name should not be more than 200.");

            RuleFor(v => v.uom)
                .NotEmpty().WithMessage("UOM is required.");

            RuleFor(v => v.Grade)
                .NotEmpty().WithMessage("Grade is required.");

            RuleFor(v => v)
                .MustAsync(BeUniqueName).WithMessage("Fuel Product must be unique.");
        }

        public async Task<bool> BeUniqueName(UpdateFuelProductCommand cmd, CancellationToken cancellationToken)
        {
            return await _context.FuelProducts.AllAsync(l => l.Name == cmd.Name && l.Id != cmd.Id , cancellationToken);
        }
    }
}
