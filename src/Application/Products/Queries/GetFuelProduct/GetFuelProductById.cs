using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Queries.GetFuelProduct
{
    public class GetFuelProductById : IRequest<FuelProductDto>
    {
        public long Id { get; set; }
    }

    public class GetFuelProductByIdHandler : IRequestHandler<GetFuelProductById,FuelProductDto>
    {
        private readonly IApplicationDbContext _contex;
        public GetFuelProductByIdHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<FuelProductDto> Handle(GetFuelProductById request, CancellationToken cancellationToken)
        {
            var fuelProduct = await _contex.FuelProducts.FindAsync(request.Id,cancellationToken);

            if(fuelProduct == null)
            {
                throw new NotFoundException("Fuel product with id "+ request.Id+" not found.");
            }

            return new FuelProductDto
            {
                Id = fuelProduct.Id,
                Name = fuelProduct.Name,
                Grade = fuelProduct.Grade,
                UOM = fuelProduct.UOM,
                IsActive = fuelProduct.IsActive,
            };
        }
    }

}
