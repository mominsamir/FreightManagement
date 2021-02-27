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
        public DateTime StartTime;
        public long DriverId;
        public long TrailerId;
        public long TruckId;
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
                throw new NotFoundException(string.Format("Truck not found with id {0}", request.TruckId));

            var trailer = await _contex.Trailers.Include(i => i.CheckLists)
                .Where(t => t.Id == request.TruckId).SingleOrDefaultAsync(cancellationToken); 

            if (trailer == null)
                throw new NotFoundException(string.Format("Trailer not found with id {0}", request.TruckId));


            //TODO:: once user instace is implemeneted , query using driver id and set object to user


            var scheduleDriver = new DriverSchedule
            {
                StartTime = request.StartTime,
                Driver = request.DriverId,
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
