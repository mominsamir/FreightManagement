using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.FirstName)
                 .NotEmpty().WithMessage("First Name is required.")
                 .MaximumLength(20).WithMessage("First Name not exceed 20 characters.");

            RuleFor(v => v.LastName)
                 .NotEmpty().WithMessage("Last Name is required.")
                 .MaximumLength(20).WithMessage("Last Name not exceed 20 characters.");

            RuleFor(v => v.Email)
                  .NotEmpty().WithMessage("Email is required.")
                 .EmailAddress().WithMessage("Enter Valid Email address.")
                 .MaximumLength(200).WithMessage("Last Name not exceed 20 characters.");

            RuleFor(v => v.Role)
                .NotEmpty().WithMessage("Role is required.")
                .MaximumLength(50).WithMessage("Number plate must not exceed 50 characters.");

            RuleFor(v => v)
                 .MustAsync(BeUniqueEmail).WithMessage("Email Address is already in Use.");
        }

        public async Task<bool> BeUniqueEmail(UpdateUserCommand user, CancellationToken cancellationToken)
        {
            return !await _context.AllUsers.AnyAsync(l => l.Email == user.Email && l.Id != user.Id, cancellationToken);
        }
    }


}
