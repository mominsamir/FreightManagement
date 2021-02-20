using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Terminal.Commands.UpdateTerminal
{
    public class UpdateRackCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IRSCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class UpdateTerminalCommandHandler : IRequestHandler<UpdateRackCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTerminalCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRackCommand request, CancellationToken cancellationToken)
        {

            var entity = await _context.Racks.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Terminal), request.Id);
            }

            entity.IRSCode = request.IRSCode;
            entity.Name = request.Name;
            entity.Address = new Address(
                    request.Street,
                    request.City,
                    request.State,
                    request.Country,
                    request.ZipCode
                );

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
