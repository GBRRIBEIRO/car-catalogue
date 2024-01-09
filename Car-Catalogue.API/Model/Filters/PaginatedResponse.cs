namespace Car_Catalogue.API.Model.Filters
{
    public class PaginatedResponse<T>
    {
        public int TotalPages { get; set; }
        public int TotalRegisters { get; set; }
        public List<T> Data { get; set; }
    }
}
