using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        private readonly PharmacyContext _context;
        public UserRepository(PharmacyContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<User> GetByUsernameAsync(string username)
        
        {
            return await _context.Users
                        .Include(u=>u.JobsTitle)
                        .FirstOrDefaultAsync(u=>u.Username.ToLower()==username.ToLower());
        }
        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users
                .Include(u => u.JobsTitle)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
        }

        
    }
}