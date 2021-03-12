using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Users.Queries.ConfirmUserIdentity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Users.Queries.UserConfiguration
{
    public class QueryUserConfiguration : IRequest<UserConfigurationDto>
    {
    }

    public class QueryUserConfigurationHandler : IRequestHandler<QueryUserConfiguration, UserConfigurationDto>
    {

        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public QueryUserConfigurationHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }


        public async Task<UserConfigurationDto> Handle(QueryUserConfiguration request, CancellationToken cancellationToken)
        {
            _ = long.TryParse(_currentUserService.UserId, out long userId);

            var user = await _context.AllUsers.FindAsync(new object[] { userId }, cancellationToken);

            if(user is null)
            {
                throw new NotFoundException($"User not found with id {userId}");
            }

            return new UserConfigurationDto
            {
                User = new UserDto(user.Id, user.FirstName, user.LastName, user.Email, user.Role,user.IsActive),
                Menus = MenuService.Get(user.Role)
            };
        }
    }
}
