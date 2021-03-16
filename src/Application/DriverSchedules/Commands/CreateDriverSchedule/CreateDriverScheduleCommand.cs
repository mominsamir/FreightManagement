using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.DriversSchedules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.DriverSchedules.Commands.CreateDriverSchedule
{
    public class CreateDriverScheduleCommand : IRequest<long>
    {
        public CreateDriverScheduleCommand(DateTime startTime, long driverId, long trailerId, long truckId)
        {
            StartTime = startTime;
            DriverId = driverId;
            TrailerId = trailerId;
            TruckId = truckId;
        }

        public DateTime StartTime { get; }
        public long DriverId{ get; }
        public long TrailerId { get; }
        public long TruckId { get; }
    }

    public class CreateDriverScheduleCommandHandler : IRequestHandler<CreateDriverScheduleCommand, long>
    {
        private readonly IApplicationDbContext _contex;
        public CreateDriverScheduleCommandHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<long> Handle(CreateDriverScheduleCommand request, CancellationToken cancellationToken)
        {

            var truck = await _contex.Trucks.Include(i=> i.CheckLists)
                .Where(t=> t.Id == request.TruckId ).SingleOrDefaultAsync(cancellationToken);

            if (truck == null)
                throw new NotFoundException($"Truck not found with id {request.TruckId}");

            var trailer = await _contex.Trailers.Include(i => i.CheckLists)
                .Where(t => t.Id == request.TrailerId).SingleOrDefaultAsync(cancellationToken); 

            if (trailer == null)
                throw new NotFoundException($"Trailer not found with id {request.TrailerId}");

            var driver = await _contex.AllUsers
                .Where(t => t.Id == request.DriverId).SingleOrDefaultAsync(cancellationToken);

            if (driver == null)
                throw new NotFoundException($"Driver not found with id {request.DriverId}");


            var scheduleDriver = new DriverSchedule
            {
                StartTime = request.StartTime,
                EndTime = request.StartTime.AddHours(8.0),
                Driver = driver,
                Truck = truck,
                Trailer = trailer,
            };

            scheduleDriver.AddCheckListNotes(truck.CheckLists);

            scheduleDriver.AddCheckListNotes(trailer.CheckLists);

            await _contex.DriverScheduleLists.AddAsync(scheduleDriver,cancellationToken);

            await _contex.SaveChangesAsync(cancellationToken);

            return scheduleDriver.Id;
        }
    }

}
