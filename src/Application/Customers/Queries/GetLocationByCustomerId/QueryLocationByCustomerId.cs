using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Customers.Queries.GetCustomerById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Queries.GetLocationByCustomerId
{
    public class QueryLocationByCustomerId: IRequest<CustomerDto>
    {
        public QueryLocationByCustomerId(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }

    public class QueryLocationByCustomerIdHandler : IRequestHandler<QueryLocationByCustomerId, CustomerDto>
    {
        public readonly IApplicationDbContext _context;

        public QueryLocationByCustomerIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDto> Handle(QueryLocationByCustomerId request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Include(l => l.Locations)
                .Where(c => c.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

            if (customer == null)
                throw new NotFoundException($"Customer not found with id {request.Id}");

            return new CustomerDto(
                    customer.Id,
                    customer.Name,
                    customer.Email.Value,
                    customer.BillingAddress,
                    customer.IsActive,
                    customer.Locations
           );
        }
    }
}
