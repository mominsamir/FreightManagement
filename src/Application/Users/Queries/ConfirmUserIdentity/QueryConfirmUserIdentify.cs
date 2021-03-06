using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
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
            var user = await _context.AllUsers.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password,cancellationToken);

            if (user is null)
            {
                throw new NotFoundException($"User Id Or Password does not match");
            }

            if (user.Password == request.Password) {
                return new UserDto(user.Id,user.FirstName,user.LastName,user.Email,user.Role);
            }
            else
            {
                return null;
            }
        }
    }
}
