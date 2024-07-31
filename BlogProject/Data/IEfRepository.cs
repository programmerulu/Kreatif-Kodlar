using BlogProject.Entites;
using System.Linq.Expressions;

namespace BlogProject.Data
{
    public interface IEfRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetById(int receivedId);
    }
}
