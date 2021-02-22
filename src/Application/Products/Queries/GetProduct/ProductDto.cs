using AutoMapper;
using FreightManagement.Application.Common.Mappings;
using FreightManagement.Domain.Entities.Products;

namespace FreightManagement.Application.Products.Queries.GetProduct
{
    public class ProductDto : IMapFrom<ProductDto>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
        }
    }
}
