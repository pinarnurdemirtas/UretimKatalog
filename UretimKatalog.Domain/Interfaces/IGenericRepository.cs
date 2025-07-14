using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(); 
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> ExistsAsync(int id);
    }
}
