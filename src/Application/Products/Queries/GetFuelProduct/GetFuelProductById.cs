using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightManagement.Application.Products.Queries.GetFuelProduct
{
    public class GetFuelProductById : IRequest<FuelProductDto>
    {
        public long Id { get; set; }
    }
}
