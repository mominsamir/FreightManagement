using FluentValidation;
using FreightManagement.Application.Common.Interfaces;

namespace FreightManagement.Application.DriverSchedules.Commands.UpdateDriverCheckList
{
    public class UpdateDriverCheckListCommandValidator : AbstractValidator<UpdateDriverCheckListCommand>
    {
        private readonly IApplicationDbContext _contex;
        public UpdateDriverCheckListCommandValidator(IApplicationDbContext contex)
        {
            _contex = contex;
        }

    }
}
