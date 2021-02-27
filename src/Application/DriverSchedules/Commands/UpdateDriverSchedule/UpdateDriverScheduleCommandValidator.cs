

using FluentValidation;
using FreightManagement.Application.Common.Interfaces;

namespace FreightManagement.Application.DriverSchedules.Commands.UpdateDriverSchedule
{
    public class UpdateDriverScheduleCommandValidator : AbstractValidator<UpdateDriverScheduleCommand>
    {
        
        private readonly IApplicationDbContext _contex;
        public UpdateDriverScheduleCommandValidator(IApplicationDbContext contex)
        {
            _contex = contex;
        }

    }

}
