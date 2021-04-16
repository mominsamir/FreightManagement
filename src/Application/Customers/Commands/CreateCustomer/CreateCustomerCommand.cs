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
        public CreateCustomerCommand(string name, string email, string street, string city, string state, string country, string zipCode)
        {
            Name = name;
            Email = email;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public string Name { get; }
        public string Email { get; }
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }
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
                         )
             };

            customer.MarkActive();

            await _contex.Customers.AddAsync(customer, cancellationToken);
            await _contex.SaveChangesAsync(cancellationToken);
            return customer.Id;
        }
    }
}
