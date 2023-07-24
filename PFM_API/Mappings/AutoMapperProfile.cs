using AutoMapper;
using PFM_API.Database.Entities;
using PFM_API.Models;

namespace PFM_API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<CategoryEntity, Category>().ForMember(et => et.Code, c => c.MapFrom(x => x.Code));
            CreateMap<Category, CategoryEntity>().ForMember(c => c.Code, et => et.MapFrom(x => x.Code));
            CreateMap<PagedSortedList<CategoryEntity>, PagedSortedList<Category>>();


            CreateMap<TransactionEntity, Transaction>().ForMember(et => et.Id, c => c.MapFrom(x => x.Id));
            CreateMap<Transaction, TransactionEntity>().ForMember(c => c.Id, et => et.MapFrom(x => x.Id));
            CreateMap<PagedSortedList<TransactionEntity>, PagedSortedList<Transaction>>();


    //        CreateMap<ProductEntity, Product>()
    //.ForMember(p => p.ProductCode, e => e.MapFrom(x => x.Code));

    //        CreateMap<Product, ProductEntity>()
    //            .ForMember(et => et.Code, p => p.MapFrom(x => x.ProductCode));

    //        //CreateMap<PagedSortedList<Product>, PagedSortedList<ProductEntity>>();
    //        CreateMap<PagedSortedList<ProductEntity>, PagedSortedList<Product>>();

    //        CreateMap<CreateProductCommand, ProductEntity>()
    //            .ForMember(et => et.Code, p => p.MapFrom(x => x.ProductCode));
        }
    }
}
