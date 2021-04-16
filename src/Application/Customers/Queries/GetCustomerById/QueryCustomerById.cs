using FreightManagement.Application.Common.Exceptions;
using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Queries.GetCustomerById
{
    public class QueryCustomerById : IRequest<ModelView<CustomerDto>> 
    {
        public long Id;

        public QueryCustomerById(long id)
        {
            Id = id;
        }
    }

    public class QueryCustomerByIdHandler : IRequestHandler<QueryCustomerById, ModelView<CustomerDto>>
    {
        public readonly IApplicationDbContext _contex;
        public QueryCustomerByIdHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<ModelView<CustomerDto>> Handle(QueryCustomerById request, CancellationToken cancellationToken)
        {
            var customer = await _contex.Customers.Include(l => l.Locations)
                .Where(c => c.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

            if(customer == null)
                throw new NotFoundException($"Customer not found with id {request.Id}");

            return new ModelView<CustomerDto>(
                new CustomerDto(
                    customer.Id,
                    customer.Name,
                    customer.Email.Value,
                    customer.BillingAddress,
                    customer.IsActive,
                    customer.Locations
                ), 
                true, 
                false, 
                true
            );
        }
    }
}
