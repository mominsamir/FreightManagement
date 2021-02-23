using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Commands.UpdateFuelProduct
{
    public class UpdateFuelProductCommand : IRequest
    {
        public long Id;
        public string Name;
        public FuelGrade Grade;
        public UnitOfMeasure uom;
        public bool IsActive;
    }

    public class UpdateFuelProductCommandHandler : IRequestHandler<UpdateFuelProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateFuelProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateFuelProductCommand request, CancellationToken cancellationToken)
        {
            var  fuelProduct = await _context.FuelProducts.FindAsync(request.Id, cancellationToken);

            if(fuelProduct == null)
            {
                throw new NotFoundException("Fuel product not found with id "+ request.Id);
            }

            fuelProduct.Name = request.Name;
            fuelProduct.Grade = request.Grade;
            fuelProduct.UOM = request.uom;
            fuelProduct.IsActive = request.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
