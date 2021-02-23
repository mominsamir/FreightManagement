using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Products;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Commands.CreateProduct
{

     public class CreateProductCommand : IRequest<long>
     {
        public string Name;
        public UnitOfMeasure uom;

     }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, long>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new FuelProduct
            {
                Name = request.Name,
                UOM = request.uom,
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
