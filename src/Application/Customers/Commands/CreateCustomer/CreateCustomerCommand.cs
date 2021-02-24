using FreightManagement.Application.Common.Interfaces;
using FreightManagement.Domain.Entities.Customers;
using FreightManagement.Domain.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreightManagement.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand  : IRequest<long> 
    {
        public string Name;
        public string Email;
        public string Street;
        public string City;
        public string State;
        public string Country;
        public string ZipCode;
    }

    public class CreateCustomerCommandHandler: IRequestHandler<CreateCustomerCommand, long>
    {
        private readonly IApplicationDbContext _contex;

        public CreateCustomerCommandHandler(IApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = new Email(request.Email),
                BillingAddress = new Address(
                             request.Street,
                             request.City,
                             request.State,
                             request.Country,
                             request.ZipCode
                         ),
            };

            await _contex.Customers.AddAsync(customer, cancellationToken);
            await _contex.SaveChangesAsync(cancellationToken);
            return customer.Id;
        }
    }
}
