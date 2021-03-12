using FluentValidation;
using FreightManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandValidator(IApplicationDbContext context)
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
                 .MaximumLength(200).WithMessage("Last Name not exceed 20 characters.")
                 .MustAsync(BeUniqueEmail).WithMessage("Email is required.");

            RuleFor(v => v.Role)
                .NotEmpty().WithMessage("Role is required.");

        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.AllUsers.AllAsync(l => l.Email!= email, cancellationToken);
        }


    }
}
