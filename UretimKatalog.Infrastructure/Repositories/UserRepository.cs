using Microsoft.EntityFrameworkCore;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;
using UretimKatalog.Infrastructure.Data;

namespace UretimKatalog.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
