using CsvHelper.Configuration;
using PFM_API.Database.Entities;

namespace PFM_API.Mappings
{
    public class TransactionsMap : ClassMap<TransactionEntity>
    {
        public TransactionsMap() {
            Map(m => m.Id).Name("id");
            Map(m => m.BeneficiaryName).Name("beneficiary-name");
            Map(m => m.Date).Name("date");
            Map(m => m.Direction).Name("direction").ToString();
            Map(m => m.Amount).Name("amount");
            Map(m => m.Description).Name("description");
            Map(m => m.CurrencyCode).Name("currency");
            Map(m => m.Mcc).Name("mcc");
            Map(m => m.Kind).Name("kind").ToString();
            //Map(m => m.category).Name("catcode");
        }
    }
}
