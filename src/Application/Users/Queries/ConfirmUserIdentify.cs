using FreightManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Users.Queries
{
    public class ConfirmUserIdentify : IRequest<UserDto>
    {
        public ConfirmUserIdentify(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public  string Password { get; }
    }

    public class ConfirmUserIdentifyHandler : IRequestHandler<ConfirmUserIdentify, UserDto>
    {
        private readonly IApplicationDbContext _context;
        public ConfirmUserIdentifyHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(ConfirmUserIdentify request, CancellationToken cancellationToken)
        {
            var user = await _context.AllUsers.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password,cancellationToken);

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
