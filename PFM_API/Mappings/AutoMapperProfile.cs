using AutoMapper;
using PFM_API.Database.Entities;
using PFM_API.Models;

namespace PFM_API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
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
