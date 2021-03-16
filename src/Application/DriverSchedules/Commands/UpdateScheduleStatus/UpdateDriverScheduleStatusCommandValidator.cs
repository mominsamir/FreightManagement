using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using FreightManagement.Domain.Entities.DriversSchedules;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.DriverSchedules.Commands.UpdateScheduleStatus
{
    public class UpdateDriverScheduleStatusCommandValidator: AbstractValidator<UpdateDriverScheduleStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDriverScheduleStatusCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Id)
                .GreaterThan(0).WithMessage("Schedule Id is not valid")
                .MustAsync(Exist).WithMessage("Schedule Id is not valid");

            RuleFor(v => v.Status)
                .IsInEnum().WithMessage("Schedule Status not valid");

            RuleFor(v => v)
                .MustAsync(IsNewStatusValid).WithMessage(v=> $"Schedule can not be marked {v.Status}");
        }

        private async Task<bool> IsNewStatusValid(UpdateDriverScheduleStatusCommand command, CancellationToken cancellationToken)
        {
            var schedule = await _context.DriverScheduleLists.Where(s => s.Id == command.Id).SingleOrDefaultAsync(cancellationToken);

            if (schedule is null) return false;

            switch (schedule.Status)
            {
                case DriverScheduleStatus.CHECKLIST_COMPLETE:
                    if(command.Status== DriverScheduleStatus.SCHEDULE_CREATED) return false;
                    break;
                case DriverScheduleStatus.SCHEDULE_CANCELLED:
                case DriverScheduleStatus.SCHEDULE_COMPLETED:
                    return false;
                default: 
                    return true;
            }
            return true;
        }

        private async  Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await _context.DriverScheduleLists.AnyAsync(s => s.Id == id, cancellationToken);
        }
    }
}
