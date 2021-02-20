using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Vehicles;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Trucks.Commands.UpdateTruckStatus
{
    public class UpdateTruckStatusCommand: IRequest<Unit>
    {
        public long Id { get; set; }
        public VehicleStatus status { get; set; }
    }

    public class UpdateTruckStatusCommandHandler : IRequestHandler<UpdateTruckStatusCommand, Unit>
    {
        private readonly IApplicationDbContext _contex;

        public UpdateTruckStatusCommandHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<Unit> Handle(UpdateTruckStatusCommand request, CancellationToken cancellationToken)
        {
            var truck = await _contex.Trucks.FindAsync(request.Id);

            if( truck == null)
            {
                throw new DllNotFoundException();
            }

            truck.Status = request.status;

            await _contex.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        Task<Unit> IRequestHandler<UpdateTruckStatusCommand, Unit>.Handle(UpdateTruckStatusCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
