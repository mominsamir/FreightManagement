using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.StorageRack;
using FreightManagement.Domain.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Terminal.Commands.CreateTerminal
{
    public class CreateTerminalCommand : IRequest<long>
    {
        public string Name { get; set; }
        public string IRSCode { get; set; }
        public string Street { get;  set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTerminalCommand, long>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateTerminalCommand request, CancellationToken cancellationToken)
        {
            var entity = new Rack
            {
                Name = request.Name,
                IRSCode = request.IRSCode,
                Address = new Address( 
                    request.Street,
                    request.City,
                    request.State,
                    request.Country,
                    request.ZipCode
                ),
                IsActive = true
            };

            _context.Racks.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
