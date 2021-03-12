using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.DriverSchedules.Commands.UpdateDriverSchedule
{
    public class UpdateDriverScheduleCommand : IRequest
    {
        public long Id;
        public DateTime EndTime;
        public long TrailerId;
        public long TruckId;
    }

    public class UpdateDriverScheduleCommandHandler: IRequestHandler<UpdateDriverScheduleCommand>
    {
        private readonly IApplicationDbContext _contex;
        public UpdateDriverScheduleCommandHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<Unit> Handle(UpdateDriverScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule =await _contex.DriverScheduleLists.FindAsync(new object[] { request.Id }, cancellationToken);
            if(schedule == null)
                 throw new NotFoundException($"Schedule not found with id {request.Id}");


            var truck = await _contex.Trucks.FindAsync(new object[] { request.TruckId }, cancellationToken);
            var trailer = await _contex.Trailers.FindAsync(new object[] { request.TrailerId }, cancellationToken);
            
            schedule.EndTime = request.EndTime;

            schedule.Truck = truck ?? throw new NotFoundException($"Truck not found with id {request.TruckId}");
            schedule.Trailer = trailer ?? throw new NotFoundException($"Trailer not found with id {request.TruckId}");

            await _contex.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
