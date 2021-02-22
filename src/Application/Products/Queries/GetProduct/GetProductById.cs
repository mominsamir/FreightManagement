using MediatR;


namespace FreightManagement.Application.Products.Queries.GetProduct
{
    public class GetProductById : IRequest<ProductDto>
    {
        public long Id { get; set; }
    }


}
