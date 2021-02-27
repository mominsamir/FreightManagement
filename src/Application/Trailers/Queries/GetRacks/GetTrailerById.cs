using AutoMapper;
using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trailers.Queries.GetRacks
{
    public class GetTrailerById : IRequest<ModelView<TrailerDto>>
    {
        public long Id { get; set; }

    }
    public class GetTrailerByIdHandler : IRequestHandler<GetTrailerById, ModelView<TrailerDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTrailerByIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ModelView<TrailerDto>> Handle(GetTrailerById request, CancellationToken cancellationToken)
        {
            var trailer = await _context.Trailers.FindAsync(new object[] { request.Id }, cancellationToken);

            if (trailer is null)
            {
                throw new NotFoundException(nameof(Trailer), request.Id);
            }

            return new ModelView<TrailerDto>(
                new TrailerDto
                {
                    Id = trailer.Id,
                    VIN = trailer.VIN,
                    NumberPlate = trailer.NumberPlate,
                    Capacity = trailer.Capacity,
                    Compartment = trailer.Compartment,
                    Status = trailer.Status,
                },true,false,true
                );
        }
    }

}
