using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Vendors;
using FreightManagement.Domain.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Vendors.Commands.CreateVendor
{
    public class CreateVendorCommand : IRequest<long>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, long>
    {
        private readonly IApplicationDbContext _context;

        public CreateVendorCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            var entity = new Vendor
            {
                Name = request.Name,
                Email = new Email(request.Email),
                Address = new Address(
                             request.Street,
                             request.City,
                             request.State,
                             request.Country,
                             request.ZipCode
                         ),
                IsActive = true 
            };

            _context.Vendors.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
