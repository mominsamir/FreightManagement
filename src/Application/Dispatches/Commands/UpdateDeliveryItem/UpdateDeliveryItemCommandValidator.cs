using FluentValidation;
using FreightManagement.Application.Common.Interfaces;


namespace FreightManagement.Application.Dispatches.Commands.UpdateDeliveryItem
{
    public class UpdateDeliveryItemCommandValidator : AbstractValidator<UpdateDeliveryItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDeliveryItemCommandValidator(IApplicationDbContext context)
        {
            _context = context;
        }
    }
}
