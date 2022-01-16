namespace ProductManagement.Domain.Products.Mappings;

using ProductManagement.Dtos.Product;
using AutoMapper;
using ProductManagement.Domain.Products;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        //createmap<to this, from this>
        CreateMap<Product, ProductDto>()
            .ReverseMap();
        CreateMap<ProductForCreationDto, Product>();
        CreateMap<ProductForUpdateDto, Product>()
            .ReverseMap();
    }
}