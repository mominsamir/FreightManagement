
using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trailers.Queries.GetTrailer
{
    public class GetTrailerById : IRequest<ModelView<TrailerDto>>
    {
        public GetTrailerById(long id)
        {
            Id = id;
        }

        public long Id { get; }

    }
    public class GetTrailerByIdHandler : IRequestHandler<GetTrailerById, ModelView<TrailerDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetTrailerByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ModelView<TrailerDto>> Handle(GetTrailerById request, CancellationToken cancellationToken)
        {
            var trailer = await _context.Trailers.Where(l=> l.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

            if (trailer is null)
                throw new NotFoundException(nameof(Trailer), request.Id);

            return new ModelView<TrailerDto>(
                new TrailerDto
                (
                    trailer.Id,
                    trailer.NumberPlate,
                    trailer.VIN,
                    trailer.Compartment,
                    trailer.Capacity,
                    trailer.Status
                ),true,false,true
            );
        }
    }

}
