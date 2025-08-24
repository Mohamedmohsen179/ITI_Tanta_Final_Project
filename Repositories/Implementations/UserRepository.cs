using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ITI_Tanta_Final_Project.Models;
using ITI_Tanta_Final_Project.Repositories.Contracts;
using ITI_Tanta_Final_Project.context;

namespace ITI_Tanta_Final_Project.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context) : base(context) 
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<User> GetById_include_teachingcoursesAsync(int id)
        {
            return _context.Users
                .Include(u => u.TeachingCourses)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(User.Role role)
        {
            return await _context.Users
                .Where(u => u.UserRole == role)
                .ToListAsync();
        }
    }
}
