using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Queries.GetProduct
{
    public class GetProductById : IRequest<ProductDto>
    {
        public long Id { get; set; }
    }

    public class GetProductByIdHandler : IRequestHandler<GetProductById, ProductDto>
    {
        private readonly IApplicationDbContext _contex;
        public GetProductByIdHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<ProductDto> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            var product = await _contex.Products.FindAsync(request.Id, cancellationToken);

            if (product == null)
            {
                throw new NotFoundException("Product with id " + request.Id + " not found.");
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                UOM = product.UOM,
                IsActive = product.IsActive,
            };
        }


    }

}
