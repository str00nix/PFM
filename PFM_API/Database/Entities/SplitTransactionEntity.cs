namespace PFM_API.Database.Entities
{
    public class SplitTransactionEntity
    {
        public int Id { get; set; }
        public string Catcode { get; set; }
        public double Amount { get; set; }
        public string? TransactionId { get; set; }
    }
}
