using Car_Catalogue.API.Model;

namespace Car_Catalogue.API.Model.Filters
{
    public class PaginatedRequest
    {
        public PaginatedRequest()
        {
            Page = 1;
            PageSize = 10;
            OrderBy = "Id";
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }


    }
}
