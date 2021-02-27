using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Queries.GetFuelProduct
{
    public class GetFuelProductById : IRequest<ModelView<FuelProductDto>>
    {
        public GetFuelProductById(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }

    public class GetFuelProductByIdHandler : IRequestHandler<GetFuelProductById, ModelView<FuelProductDto>>
    {
        private readonly IApplicationDbContext _contex;
        public GetFuelProductByIdHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<ModelView<FuelProductDto>> Handle(GetFuelProductById request, CancellationToken cancellationToken)
        {
            var fuelProduct = await _contex.FuelProducts.FindAsync(new object[] { request.Id }, cancellationToken);

            if(fuelProduct == null)
                throw new NotFoundException($"Fuel product with id {request.Id} not found.");

            return new ModelView<FuelProductDto>(
                new FuelProductDto
                {
                    Id = fuelProduct.Id,
                    Name = fuelProduct.Name,
                    Grade = fuelProduct.Grade,
                    UOM = fuelProduct.UOM,
                    IsActive = fuelProduct.IsActive,
                }, true,false, true
            );
        }
    }

}
