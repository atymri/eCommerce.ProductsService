using AutoMapper;
using eCommerce.DataAccessLayer.Entitie;

namespace eCommerce.BusinessLogicLayer.DTOs.MappingProfiles;

public class ProductMappingProfile : Profile 
{
    public ProductMappingProfile()
    {
        CreateMap<ProductAddRequest, Product>()
            .ForMember(dst => dst.ProductName, opt => opt.MapFrom(src => src.productName))
            .ForMember(dst => dst.UnitPrice, opt => opt.MapFrom(src => src.unitPrice))
            .ForMember(dst => dst.QuantityInStock, opt => opt.MapFrom(src => src.quantityInStock))
            .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.category))
            .ForMember(dst => dst.ProductID, opt => opt.Ignore());

        CreateMap<ProductUpdateRequest, Product>()
            .ForMember(dst => dst.ProductID, opt => opt.MapFrom(src => src.productId))
            .ForMember(dst => dst.ProductName, opt => opt.MapFrom(src => src.productName))
            .ForMember(dst => dst.UnitPrice, opt => opt.MapFrom(src => src.unitPrice))
            .ForMember(dst => dst.QuantityInStock, opt => opt.MapFrom(src => src.quantityInStock))
            .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.category));

        CreateMap<Product, ProductRersponse>()
            .ForMember(dst => dst.productID, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dst => dst.productName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dst => dst.unitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dst => dst.quantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
            .ForMember(dst => dst.category, opt => opt.MapFrom(src => src.Category));

        CreateMap<ProductRersponse, Product>()
            .ForMember(dst => dst.ProductID, opt => opt.MapFrom(src => src.productID))
            .ForMember(dst => dst.ProductName, opt => opt.MapFrom(src => src.productName))
            .ForMember(dst => dst.UnitPrice, opt => opt.MapFrom(src => src.unitPrice))
            .ForMember(dst => dst.QuantityInStock, opt => opt.MapFrom(src => src.quantityInStock))
            .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.category.ToString()));

    }
}

