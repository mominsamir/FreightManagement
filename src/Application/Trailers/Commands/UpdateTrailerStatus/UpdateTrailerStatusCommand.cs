using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trailers.Commands.UpdateTrailerStatus
{
    public  class UpdateTrailerStatusCommand   : IRequest<Unit>
    {
        public long Id { get; set; }
        public VehicleStatus status { get; set; }
    }

    public class UpdateTrailerStatusCommandHandler : IRequestHandler<UpdateTrailerStatusCommand, Unit>
    {
        private readonly IApplicationDbContext _contex;

        public UpdateTrailerStatusCommandHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<Unit> Handle(UpdateTrailerStatusCommand request, CancellationToken cancellationToken)
        {
            var truck = await _contex.Trailers.FindAsync(new object[] { request.Id }, cancellationToken);

            if (truck == null)
            {
                throw new DllNotFoundException();
            }

            switch (request.status)
            {
                case VehicleStatus.ACTIVE:
                    truck.MakeActive();
                    break;
                case VehicleStatus.OUT_OF_SERVICE:
                    truck.MakeOutOfService();
                    break;
                case VehicleStatus.UNDER_MAINTNCE:
                    truck.MakeUnderMaintanace();
                    break;
            }

            await _contex.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


}
