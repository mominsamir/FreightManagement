using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trucks.Queries
{
    public class GetTruckById : IRequest<ModelView<TruckDto>>
    {
        public GetTruckById(long id)
        {
            Id = id;
        }

        public long Id { get;  }

    }
    public class GetTruckByIdHandler : IRequestHandler<GetTruckById, ModelView<TruckDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetTruckByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ModelView<TruckDto>> Handle(GetTruckById request, CancellationToken cancellationToken)
        {

            var truck = await _context.Trucks.Where (l=> l.Id == request.Id ).SingleOrDefaultAsync(cancellationToken);

            if (truck  == null)
            {
                throw new NotFoundException(nameof(Truck), request.Id);
            }

            return new ModelView<TruckDto>(
                    new TruckDto(
                    truck.Id,
                    truck.NumberPlate,
                    truck.VIN,
                    truck.NextMaintanceDate,
                    truck.Status)
                    ,true,false, true
            );
        }
    }

}
