namespace SampleProductInventoryApi.Models
{
    public class ProductQueryParameters : PaginationQueryParameters
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
