using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;
using UretimKatalog.Infrastructure.Data;

namespace UretimKatalog.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context) => _context = context;

        public IQueryable<T> GetAll() => _context.Set<T>().AsNoTracking();
        public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Remove(T entity) => _context.Set<T>().Remove(entity);
    }
}