using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public readonly IApplicationDbContext _context;
        private readonly ILogger _logger;


        public CreateProductCommandValidator(IApplicationDbContext context, ILogger<CreateProductCommandValidator> logger)
        {
            _context = context;
            _logger = logger;

            _logger.Log(LogLevel.Information, "Product received ");

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name should not be more than 200.")
                .MustAsync(BeUniqueName).WithMessage("Name must be Unique.");

            RuleFor(v => v.UOM)
                .NotNull().WithMessage("UOM is required.");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Products.AllAsync(l => l.Name == name, cancellationToken);
        }
    }
}
