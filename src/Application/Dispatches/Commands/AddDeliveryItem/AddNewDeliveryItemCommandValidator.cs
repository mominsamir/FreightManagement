using FluentValidation;
using FreightManagement.Application.Common.Interfaces;

namespace FreightManagement.Application.Dispatches.Commands.AddDeliveryItem
{
    public class AddNewDeliveryItemCommandValidator : AbstractValidator<AddNewDeliveryItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public AddNewDeliveryItemCommandValidator(IApplicationDbContext context)
        {
            _context = context;
        }

    }
}
