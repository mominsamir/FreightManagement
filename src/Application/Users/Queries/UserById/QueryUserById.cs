using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using FreightManagement.Application.Users.Queries.ConfirmUserIdentity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Users.Queries.UserById
{
    public class QueryUserById : IRequest<ModelView<UserDto>>
    {
        public QueryUserById(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }

    public class QueryUserByIdHandler : IRequestHandler<QueryUserById, ModelView<UserDto>>
    {
        private readonly IApplicationDbContext _context;

        public QueryUserByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ModelView<UserDto>> Handle(QueryUserById request, CancellationToken cancellationToken)
        {
            var user = await _context.AllUsers.FindAsync(new object[] { request.Id }, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException($"User not found with id {request.Id}");
            }

            return new ModelView<UserDto>(new UserDto(user.Id, user.FirstName, user.LastName, user.Email, user.Role, user.IsActive), true, false, true);

        }
    }

}
