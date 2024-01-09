using Car_Catalogue.API.Data;

namespace Car_Catalogue.API.Model.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<List<T>> GetAll();
        public Task<T> GetById(string id);
        public Task Post(T obj);
        public Task Put(T obj);
        public Task Delete(T obj);
    }
}
