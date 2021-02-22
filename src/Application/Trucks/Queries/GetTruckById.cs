using AutoMapper;
using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trucks.Queries
{
    public class GetTruckById : IRequest<TruckDto>
    {
        public long Id { get; set; }

    }
    public class GetTruckByIdHandler : IRequestHandler<GetTruckById, TruckDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTruckByIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TruckDto> Handle(GetTruckById request, CancellationToken cancellationToken)
        {
            var trailer = await _context.Trailers.FindAsync(request.Id);

            if (trailer == null)
            {
                throw new NotFoundException(nameof(Truck), request.Id);
            }

            /*            return _mapper.Map<RackDto>(rack);*/
            return new TruckDto
            {
                Id = trailer.Id,
                VIN = trailer.VIN,
                NumberPlate = trailer.NumberPlate,
                Status = trailer.Status,
            };
        }
    }

}
