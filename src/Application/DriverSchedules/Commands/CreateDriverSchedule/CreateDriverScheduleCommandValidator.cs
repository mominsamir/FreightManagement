using FluentValidation;
using FreightManagement.Application.Common.Interfaces;


namespace FreightManagement.Application.DriverSchedules.Commands.CreateDriverSchedule
{
    public class CreateDriverScheduleCommandValidator : AbstractValidator<CreateDriverScheduleCommand>
    {
        private readonly IApplicationDbContext _contex;

        public CreateDriverScheduleCommandValidator(IApplicationDbContext contex)
        {
            _contex = contex;
        }
    }
}
