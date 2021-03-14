using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Users.Queries.ConfirmUserIdentity;
using FreightManagement.Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FreightManagement.Application.Common.Extentions;
using Microsoft.Extensions.Logging;


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
        private readonly ILogger _logger;

        public QueryUserSearchHandler(IApplicationDbContext contex, ILogger<QueryUserSearch> logger)
        {
            _contex = contex;
            _logger = logger;
        }

        public async Task<PaginatedList<UserDto>> Handle(QueryUserSearch request, CancellationToken cancellationToken)
        {

            _logger.LogError($"User search Paging Request => {request.Page} => {request.PageSize} => {request.FilterData.Count()}");

            var query = _contex.AllUsers;

            // add where clause
            foreach (var f in request.SortData)
            {
                _logger.LogDebug($"SORT ORDER XXXXXXXXXXXXXXXXXXXXXXXX => {f.Column}=> {f.SortOrder} XXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            }
            foreach (var f in request.FilterData)
            {
                _logger.LogDebug($" FILTER XXXXXXXXXXXXXXXXXXXXXXXX => {f.Name}=> {f.Operator} => {f.Value} XXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            }
            // add sort clause


            var queryFields = query.AsQueryable().Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .WhereRules(request.FilterData)
                .OrderByPropertyOrField(nameof(User.FirstName),false);

            if (!request.FilterData.Any())
            {
//                queryFields = queryFields;
            }
            else
            {
//                queryFields = queryFields.WhereRules(request.FilterData);
            }


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

/*
Here's an example using System.Linq.Expressions. Although the example here is specific to your Claim class you can make functions like this generic and then use them to build predicates dynamically for all your entities. I've been using recently to provide users with a flexible search for entities on any entity property (or groups of properties) function without having to hard code all the queries.

public Expression<Func<Claim, Boolean>> GetClaimWherePredicate(String name, String ssn)
{
  //the 'IN' parameter for expression ie claim=> condition
  ParameterExpression pe = Expression.Parameter(typeof(Claim), "Claim");

  //Expression for accessing last name property
  Expression eLastName = Expression.Property(pe, "ClaimantLastName");

  //Expression for accessing ssn property
  Expression eSsn = Expression.Property(pe, "ClaimantSSN");

  //the name constant to match 
  Expression cName = Expression.Constant(name);

  //the ssn constant to match 
  Expression cSsn = Expression.Constant(ssn);

  //the first expression: ClaimantLastName = ?
  Expression e1 = Expression.Equal(eLastName, cName);

  //the second expression:  ClaimantSSN = ?
  Expression e2 = Expression.Equal(eSsn, cSsn);

  //combine them with and
  Expression combined = Expression.And(e1, e2);

  //create and return the predicate
  return Expression.Lambda<Func<Claim, Boolean>>(combined, new ParameterExpression[] { pe });
}

*/