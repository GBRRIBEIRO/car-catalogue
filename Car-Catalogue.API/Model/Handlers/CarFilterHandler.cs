using Car_Catalogue.API.Data;
using Car_Catalogue.API.Model;
using Car_Catalogue.API.Model.Filters;
using Microsoft.EntityFrameworkCore;

namespace Car_Catalogue.API.Model.Handlers
{
    public static class CarFilterHandler
    {
        public static async Task<List<Car>> GetFiltered(this CarFilterRequest request, ApplicationDbContext dbContext)
        {
            var cars = dbContext.Set<Car>().AsQueryable();

            if (request.BrandName != null) cars = cars.Where(car => car.Brand.ToUpper() == request.BrandName.ToUpper());
            if (request.isManual != null) cars = cars.Where(car => car.isManual == request.isManual);
            if (request.Model != null) cars = cars.Where(car => car.Model.ToUpper() == request.Model.ToUpper());
            if (request.MinPrice != null) cars = cars.Where(car => car.Price >= request.MinPrice);
            if (request.MaxPrice != null) cars = cars.Where(car => car.Price <= request.MaxPrice);

            var result = await cars.ToListAsync();
            return result;
        }
    }
}
