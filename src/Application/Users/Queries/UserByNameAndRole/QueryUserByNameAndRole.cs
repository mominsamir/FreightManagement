using FreightManagement.Application.Common.Extentions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        public QueryUserByNameAndRoleHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<UserByRoleDto>> Handle(QueryUserByNameAndRole request, CancellationToken cancellationToken)
        {
            var query = _context.AllUsers.AsNoTracking().AsQueryable();

            var filters  = new List<Filter>(){ 
                new Filter(nameof(User.FirstName), request.Name, FieldOperator.STARTS_WITH.ToString()),
                new Filter(nameof(User.Role), request.Role, FieldOperator.EQUAL.ToString()) 
            };

            var result = await query
                .WhereRules(filters)
                .OrderBy(u => u.FirstName)
                .Select(u => new UserByRoleDto(u.Id, u.FirstName, u.LastName, u.Email, u.IsActive))
                .ToListAsync(cancellationToken);

            if (result.Any()) 
                return new PaginatedList<UserByRoleDto>(result, 1, 1, 1);
            else
                return new PaginatedList<UserByRoleDto>(new List<UserByRoleDto>(), 1, 1, 1);
        }
    }

}
