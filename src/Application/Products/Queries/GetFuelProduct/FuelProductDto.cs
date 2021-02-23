using AutoMapper;
using FreightManagement.Application.Common.Mappings;
using FreightManagement.Domain.Entities.Products;


namespace FreightManagement.Application.Products.Queries.GetFuelProduct
{
   public  class FuelProductDto : IMapFrom<FuelProductDto>
   {
        public long Id { get; set; }
        public string Name { get; set; }
        public FuelGrade Grade { get; set; }
        public UnitOfMeasure UOM { get; set; }
        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FuelProduct, FuelProductDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
        }

    }
}
