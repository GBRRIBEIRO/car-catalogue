using Car_Catalogue.API.Model.Filters;

namespace Car_Catalogue.API.Model.Handlers
{
    public static class PaginationHandler
    {
        //public static TResponse GetResponse<TResponse, T>(IQueryable<T> query, PaginatedFilteredRequest request) where TResponse : PaginatedFilteredResponse<T>, new()
        //{
        //    //Prepares the request using as parameter the response and the request object, such as their classes
        //    //Create a new response
        //    var response = new TResponse();
        //    //Count all the 
        //    var count = query.Count();
        //    //Get total pages
        //    response.TotalPages = (int)Math.Abs((double)count / request.PageSize);
        //    response.TotalRegisters = count;

        //    //Checks if the property orderby is null
        //    if (string.IsNullOrEmpty(request.OrderBy)) response.Data = query.ToList();
        //    else
        //    {
        //        response.Data = query.OrderByDynamic<T>(request.OrderBy).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
        //    }

        //}


        //TResponse = generic response
        //T = generic data type
        public static TResponse GetPaginatedResponse<TResponse, T>(IEnumerable<T> data, PaginatedRequest request) where TResponse : PaginatedResponse<T>, new()
        {
            TResponse response = new TResponse();
            response.TotalRegisters = data.Count();
            response.Data = data.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).OrderByDynamic(request.OrderBy).ToList();
            response.TotalPages = (int)Math.Abs((double)response.TotalRegisters / request.PageSize);
            if (response.TotalPages == 0) response.TotalPages = 1;

            return response;
        }
        private static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, string propertyName)
        {
            if (propertyName[0] == '-')
            {
                propertyName = propertyName.Remove(0, 1);
                return query.OrderByDescending(o => o.GetType().GetProperty(propertyName).GetValue(o, null));
            }

            return query.OrderBy(o => o.GetType().GetProperty(propertyName).GetValue(o, null));
        }
    }
}
