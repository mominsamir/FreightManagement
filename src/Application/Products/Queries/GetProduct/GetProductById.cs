using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Queries.GetProduct
{
    public class GetProductById : IRequest<ModelView<ProductDto>>
    {
        public long Id { get; set; }
    }

    public class GetProductByIdHandler : IRequestHandler<GetProductById, ModelView<ProductDto>>
    {
        private readonly IApplicationDbContext _contex;
        public GetProductByIdHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<ModelView<ProductDto>> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            var product = await _contex.Products.FindAsync(new object[] { request.Id }, cancellationToken);

            if (product == null)
                throw new NotFoundException($"Product with id {request.Id} not found.");

            return new ModelView<ProductDto>(
                new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    UOM = product.UOM,
                    IsActive = product.IsActive,
                },
                true,
                false,
                true
           );

        }


    }

}
