using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Products;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Commands.CreateProduct
{

     public class CreateProductCommand : IRequest<long>
     {
        public CreateProductCommand(string name, UnitOfMeasure uOM)
        {
            Name = name;
            UOM = uOM;
        }

        public string Name { get; }
        public UnitOfMeasure UOM { get; }

     }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, long>
    {
        private readonly ILogger _logger;
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context, ILogger<CreateProductCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<long> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, "Product received " + request.Name);
            var product = new Product
            {
                Name = request.Name,
                UOM = request.UOM,
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
