using System.Threading.Tasks;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
