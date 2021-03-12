using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Security;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Users.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public ResetPasswordCommand(long id,  string password, string confirmPassword)
        {
            Id = id;
            ConfirmPassword = confirmPassword;
            Password = password;
        }
        public long Id { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IApplicationDbContext _context;

        public ResetPasswordCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var ve = new ValidationException();

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                ve.Errors.Add("Password", new string[] { "Password is required" });
                throw ve;
            }

            if (request.Password.Length < 10)
            {
                ve.Errors.Add("Password", new string[] { "Password must be atleast 10 char long" });
                throw ve;
            }

            if (request.Password == request.ConfirmPassword)
            {
                ve.Errors.Add("Password", new string[] { "Password and confirm password doesnt match" });
                throw ve;
            }

            var user = await _context.AllUsers.FindAsync(new object[] { request.Id }, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException($"User not found with uid {request.Id}");
            }

            user.Password = PasswordEncoder.ConvertPasswordToHash(request.Password);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
