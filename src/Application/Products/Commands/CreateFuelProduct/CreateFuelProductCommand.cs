using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Products;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Commands.CreateFuelProduct
{
   public class CreateFuelProductCommand : IRequest<long> 
   {
        public string Name;
        public FuelGrade Grade;
        public UnitOfMeasure uom;

    }

    public class CreateFuelProductCommandHandler : IRequestHandler<CreateFuelProductCommand, long>
    {
        private readonly IApplicationDbContext _context;
        public CreateFuelProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateFuelProductCommand request, CancellationToken cancellationToken)
        {
            var product = new FuelProduct
            {
                Name = request.Name,
                Grade = request.Grade,
                UOM = request.uom,
            };

            _context.FuelProducts.Add(product);

           await  _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
