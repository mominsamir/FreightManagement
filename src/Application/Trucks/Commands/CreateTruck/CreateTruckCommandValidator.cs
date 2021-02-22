using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trucks.Commands.CreateTruck
{
    public class CreateTruckCommandValidator : AbstractValidator<CreateTruckCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateTruckCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.VIN)
                 .NotEmpty().WithMessage("VIN # is required.")
                 .MaximumLength(200).WithMessage("VIN # must not exceed 50 characters.")
                 .MustAsync(BeUniqueVIN).WithMessage("VIN # already exists.");

            RuleFor(v => v.NumberPlate)
                .NotEmpty().WithMessage("Number plate is required.")
                .MaximumLength(50).WithMessage("Number plate must not exceed 50 characters.")
                .MustAsync(BeUniqueNumberPlate).WithMessage("Number plate already exists.");

        }

        public async Task<bool> BeUniqueVIN(string vin, CancellationToken cancellationToken)
        {
            return await _context.Trucks.AllAsync(l => l.VIN!= vin, cancellationToken);
        }
        public async Task<bool> BeUniqueNumberPlate(string number, CancellationToken cancellationToken)
        {
            return await _context.Trucks.AllAsync(l => l.NumberPlate != number, cancellationToken);
        }
    }
}
