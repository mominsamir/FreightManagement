using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trailer.Commands.UpdateTrailer
{
    public class UpdateTrailerCommand : IRequest
    {
        public long Id { get; set; }
        public string NumberPlate { get; set; }
        public string VIN { get; set; }
        public double Capacity { get; set; }
        public int Compartment { get; set; }
        public VehicleStatus Status { get; set; }
    }

    public class UpdateTrailerCommandHandler : IRequestHandler<UpdateTrailerCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateTrailerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateTrailerCommand request, CancellationToken cancellationToken)
        {
            var trailer = await _context.Trailers.FindAsync(request.Id);

            if(trailer == null)
            {
                throw new NotImplementedException();
            }

            trailer.VIN = request.VIN;
            trailer.NumberPlate = request.NumberPlate;
            trailer.Capacity = request.Capacity;
            trailer.Compartment = request.Compartment;
            trailer.Status = request.Status;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
