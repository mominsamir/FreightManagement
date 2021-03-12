using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Users.Queries.ConfirmUserIdentity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


//https://www.codeproject.com/Articles/5260863/Translating-Csharp-Lambda-Expressions-to-General-P

namespace FreightManagement.Application.Users.Queries.UserSearch
{
    public class QueryUserSearch : IRequest<PaginatedList<UserDto>>
    {
        public QueryUserSearch(
            int page, 
            int pageSize,
            IEnumerable<Dictionary<string, string>> sortData, 
            IEnumerable<Filter> filterData
        )
        {
            Page = page;
            PageSize = pageSize;
            SortData = sortData;
            FilterData = filterData;
        }

        public int Page { get; } = 1;
        public int PageSize { get; } = 10;
        public IEnumerable<Dictionary<string, string>> SortData { get; }
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

            /*            return await _contex.AllUsers
                            .OrderBy(x => x.FirstName)
                            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                            .PaginatedListAsync(request.Page, request.PageSize);
            */
            var query = _contex.AllUsers;

            // add where clause
/*                query.Where(FilterByPk());*/
                        // add sort clause

                        var data =  await query.Skip((request.Page - 1)* request.PageSize)
                            .Take(request.PageSize)
                            .Select(user=> 
                                new UserDto(user.Id, user.FirstName, user.LastName, user.Email, user.Role,user.IsActive)
                             )
                            .ToListAsync(cancellationToken: cancellationToken);

                        var count = await query.CountAsync(cancellationToken: cancellationToken);

                        return new PaginatedList<UserDto>(data, count, 1, 1);
            
        }

        /*        protected Expression<Func<T, bool>> FilterByPk(T t, string field, string value)
                {
                     var bools = String.Contains("");
                    ParameterExpression entity = Expression.Parameter(typeof(t), "entity");
                    Expression keyValue = Expression.Property(entity,"Field");
                    Expression pkValue = Expression.Call(keyValue, typeof(string).GetMethod("Contaiain",
                                    new[] { typeof(string) }));
                    Expression body = Expression.Equal(keyValue,pkValue);
                    return Expression.Lambda<Func<User, bool>>(body, entity);
                }

                private void buildFilters(T entity, IEnumerable<Filter> filterData)
                {
                    foreach(var data in filterData){

                    }
                }
        */


    }



}
