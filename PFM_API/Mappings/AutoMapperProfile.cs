using AutoMapper;
using PFM_API.Database.Entities;
using PFM_API.Models;

namespace PFM_API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            //CreateMap<CategoryEntity, Category>().ForMember(et => et.Code, c => c.MapFrom(x => x.Code));
            //CreateMap<Category, CategoryEntity>().ForMember(c => c.Code, et => et.MapFrom(x => x.Code));
            //CreateMap<PagedSortedList<CategoryEntity>, PagedSortedList<Category>>();


            //CreateMap<TransactionEntity, Transaction>().ForMember(et => et.Id, c => c.MapFrom(x => x.Id));
            //CreateMap<Transaction, TransactionEntity>().ForMember(c => c.Id, et => et.MapFrom(x => x.Id));
            //CreateMap<PagedSortedList<TransactionEntity>, PagedSortedList<Transaction>>();


            CreateMap<Category, CategoryEntity>();
            CreateMap<CategoryEntity, Category>();

            CreateMap<PagedSortedList<CategoryEntity>, PagedSortedList<Category>>();
            CreateMap<PagedSortedList<Category>, PagedSortedList<CategoryEntity>>();

            CreateMap<TransactionEntity, Transaction>();
            CreateMap<Transaction, TransactionEntity>();

            CreateMap<PagedSortedList<TransactionEntity>, PagedSortedList<Transaction>>();
            CreateMap<PagedSortedList<Transaction>, PagedSortedList<TransactionEntity>>();

            CreateMap<SplitTransaction, SplitTransactionEntity>();
            CreateMap<SplitTransactionEntity, SplitTransaction>();
        }
    }
}
