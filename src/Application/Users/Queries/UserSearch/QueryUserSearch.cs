using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Users.Queries.ConfirmUserIdentity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreightManagement.Application.Common.Extentions;


//https://www.codeproject.com/Articles/5260863/Translating-Csharp-Lambda-Expressions-to-General-P

namespace FreightManagement.Application.Users.Queries.UserSearch
{
    public class QueryUserSearch : IRequest<PaginatedList<UserDto>>
    {
        public QueryUserSearch(
            int page, 
            int pageSize,
            IEnumerable<Filter> filterData,
            IEnumerable<Sort> sortData 
        )
        {
            Page = page;
            PageSize = pageSize;
            SortData = sortData;
            FilterData = filterData;
        }

        public int Page { get; } = 1;
        public int PageSize { get; } = 10;
        public IEnumerable<Sort> SortData { get; }
        public IEnumerable<Filter> FilterData { get; } = new List<Filter>();
    }

    public class QueryUserSearchHandler : IRequestHandler<QueryUserSearch, PaginatedList<UserDto>>
    {
        private readonly IApplicationDbContext _contex;

        public QueryUserSearchHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<PaginatedList<UserDto>> Handle(QueryUserSearch request, CancellationToken cancellationToken)
        {

            var query = _contex.AllUsers;

            var queryFields = query.AsQueryable().Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .WhereRules(request.FilterData)
                .OrderByColumns(request.SortData);

            var result = await queryFields
                    .ToListAsync(cancellationToken: cancellationToken);

            var count = await queryFields.CountAsync(cancellationToken: cancellationToken);

            return new PaginatedList<UserDto>(result.Select(user =>
                    new UserDto(user.Id, user.FirstName, user.LastName, user.Email, user.Role, user.IsActive)
                 ).ToList()
                 , count, 1, 1);
            
        }

    }

}
