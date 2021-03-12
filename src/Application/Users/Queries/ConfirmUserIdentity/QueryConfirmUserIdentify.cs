using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Users.Queries.ConfirmUserIdentity
{
    public class QueryConfirmUserIdentify : IRequest<UserDto>
    {
        public QueryConfirmUserIdentify(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public  string Password { get; }
    }

    public class ConfirmUserIdentifyHandler : IRequestHandler<QueryConfirmUserIdentify, UserDto>
    {
        private readonly IApplicationDbContext _context;

        public ConfirmUserIdentifyHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(QueryConfirmUserIdentify request, CancellationToken cancellationToken)
        {
            var user = await _context.AllUsers.FirstOrDefaultAsync(u => u.Email == request.Email,cancellationToken);

            if (user is null)
            {
                throw new ForbiddenAccessException();
            }

            if (PasswordEncoder.ComparePassword(request.Password, user.Password)) {
                return new UserDto(user.Id,user.FirstName,user.LastName,user.Email,user.Role,user.IsActive);
            }
            else
            {
                throw new ForbiddenAccessException();
            }
        }
    }
}
