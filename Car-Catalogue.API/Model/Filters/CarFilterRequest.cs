using Car_Catalogue.API.Model;

namespace Car_Catalogue.API.Model.Filters
{
    public class CarFilterRequest : PaginatedRequest
    {
        public string? BrandName { get; set; }
        public string? Model { get; set; }
        public bool? isManual { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

    }
}
