using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Users.Queries.UserByNameAndRole
{
    public class QueryUserByNameAndRole: IRequest<PaginatedList<UserByRoleDto>>
    {
        public QueryUserByNameAndRole(string name, string role)
        {
            Name = name;
            Role = role;
        }

        public string Name { get; }
        public string Role { get; }
    }

    public class QueryUserByNameAndRoleHandler : IRequestHandler<QueryUserByNameAndRole, PaginatedList<UserByRoleDto>>
    {
        private readonly IApplicationDbContext _context;
        private ILogger _logger;

        public QueryUserByNameAndRoleHandler(IApplicationDbContext context, ILogger<QueryUserByNameAndRole> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PaginatedList<UserByRoleDto>> Handle(QueryUserByNameAndRole request, CancellationToken cancellationToken)
        {
            var query = _context.AllUsers.AsNoTracking();

            var result = await query
                .Where(u => u.FirstName.Contains(request.Name) && u.Role == request.Role)
                .OrderBy(u => u.FirstName)
                .ToListAsync(cancellationToken);

            if (!result.Any()) { 
                var resultDto = result.Select(u => new UserByRoleDto(u.Id, u.FirstName, u.LastName, u.Email, u.IsActive)).ToList();
                return new PaginatedList<UserByRoleDto>(resultDto, 1, 1, 1);
            }
            else
                return new PaginatedList<UserByRoleDto>(new List<UserByRoleDto>(), 1, 1, 1);
        }
    }

}
