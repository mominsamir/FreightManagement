using FreightManagement.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;
/*        private readonly IIdentityService _identityService;*/

        public LoggingBehaviour(IApplicationDbContext context, ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _context = context;
/*            _identityService = identityService;*/
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId =  _currentUserService.UserId ?? string.Empty;
            string userName = _currentUserService.UserName ?? string.Empty;

            if(!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(userName))
            {
                var user = await _context.AllUsers.FindAsync(new object[] { long.Parse(userId) }, cancellationToken);
                userName = $"{user.FirstName} {user.LastName}";
            }

            _logger.LogInformation("FreightManagement Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }
    }
}
