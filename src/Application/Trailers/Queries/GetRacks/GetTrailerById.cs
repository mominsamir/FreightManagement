using AutoMapper;
using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trailers.Queries.GetRacks
{
    public class GetTrailerById : IRequest<TrailerDto>
    {
        public long Id { get; set; }

    }
    public class GetTrailerByIdHandler : IRequestHandler<GetTrailerById, TrailerDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTrailerByIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TrailerDto> Handle(GetTrailerById request, CancellationToken cancellationToken)
        {
            var trailer = await _context.Trailers.FindAsync(request.Id);

            if (trailer == null)
            {
                throw new NotFoundException(nameof(Trailer), request.Id);
            }

            /*            return _mapper.Map<RackDto>(rack);*/
            return new TrailerDto
            {
                Id = trailer.Id,
                VIN = trailer.VIN,
                NumberPlate = trailer.NumberPlate,
                Capacity = trailer.Capacity,
                Compartment = trailer.Compartment,
                Status = trailer.Status,
            };
        }
    }

}
