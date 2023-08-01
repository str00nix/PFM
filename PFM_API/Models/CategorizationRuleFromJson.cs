namespace PFM_API.Models
{
    public class CategorizationRuleFromJson
    {
        public string Name { get; set; }
        public string CatCode { get; set; }
        public string ParentCode { get; set; }
        public List<string> Keywords { get; set; }
        public List<int>? MCC { get; set; }
    }
}
