using AutoMapper;
using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
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
        private readonly IMapper _mapper;

        public GetTruckByIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ModelView<TruckDto>> Handle(GetTruckById request, CancellationToken cancellationToken)
        {
            var trailer = await _context.Trucks.FindAsync(new object[] { request.Id }, cancellationToken);

            if (trailer == null)
            {
                throw new NotFoundException(nameof(Truck), request.Id);
            }

            return new ModelView<TruckDto>(
                    new TruckDto{
                    Id = trailer.Id,
                    VIN = trailer.VIN,
                    NumberPlate = trailer.NumberPlate,
                    Status = trailer.Status}
                    ,true,false, true
            );
        }
    }

}
