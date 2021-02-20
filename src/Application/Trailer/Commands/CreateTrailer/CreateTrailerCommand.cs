using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trailer.Commands.CreateTrailer
{
    public class CreateTrailerCommand : IRequest<long>
    {
        public string NumberPlate { get; set; }
        public string VIN { get; set; }
        public double Capacity { get; set; }
        public int Compartment { get; set; }
    }

    public class CreateTrailerCommandHandler : IRequestHandler<CreateTrailerCommand, long>
    {
        public readonly IApplicationDbContext _context;
        public CreateTrailerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateTrailerCommand request, CancellationToken cancellationToken)
        {
            var trailer = new Domain.Entities.Vehicles.Trailer
            {
                NumberPlate = request.NumberPlate,
                VIN = request.VIN,
                Capacity = request.Capacity,
                Compartment = request.Compartment,
                Status = VehicleStatus.ACTIVE
            };

            _context.Trailers.Add(trailer);

            await _context.SaveChangesAsync(cancellationToken);

            return trailer.Id;
        }
    }
}
