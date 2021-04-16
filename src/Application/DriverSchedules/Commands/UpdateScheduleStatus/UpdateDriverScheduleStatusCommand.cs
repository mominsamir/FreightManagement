using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.DriversSchedules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.DriverSchedules.Commands.UpdateScheduleStatus
{
    public class UpdateDriverScheduleStatusCommand : IRequest<long> 
    {
        public UpdateDriverScheduleStatusCommand(long id, DriverScheduleStatus status)
        {
            Id = id;
            Status = status;
        }

        public long Id { get; }
        public DriverScheduleStatus Status { get; }
    }

    public class UpdateDriverScheduleStatusCommandHandler : IRequestHandler<UpdateDriverScheduleStatusCommand, long>
    {

        private readonly IApplicationDbContext _context;

        public UpdateDriverScheduleStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(UpdateDriverScheduleStatusCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _context.DriverScheduleLists
                 .Where(l => l.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

            if (schedule == null)
                throw new NotFoundException($"Schedule not found with id {request.Id}");

            if(request.Status == DriverScheduleStatus.SCHEDULE_COMPLETED)
                schedule.TryCompleteCheckList();
            else if (request.Status == DriverScheduleStatus.SCHEDULE_CANCELLED)
                schedule.TryCancelSchedule();
            else
            {
                var ve = new ValidationException();
                ve.Errors.Add("status",new string[] { $"Status can not be changed {request.Status}" });
                throw ve;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return schedule.Id;
        }
    }
}
