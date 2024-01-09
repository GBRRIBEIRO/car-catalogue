using Car_Catalogue.API.Data;
using Car_Catalogue.API.Model.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car_Catalogue.API.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _dbSet;
        public ApplicationDbContext _dbContext { get; set; }

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> GetById(string id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task Delete(T obj)
        {
            _dbSet.Remove(obj);
            await _dbContext.SaveChangesAsync();
        }


        public async Task Post(T obj)
        {
            await _dbSet.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Put(T obj)
        {
            _dbSet.Update(obj);
            await _dbContext.SaveChangesAsync();
        }

        //public PaginatedResponse<T> GetPaginated(PaginatedRequest paginatedRequest)
        //{
        //    _dbSet.
        //}
    }
}
