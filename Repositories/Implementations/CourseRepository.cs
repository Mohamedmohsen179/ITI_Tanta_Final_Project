using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ITI_Tanta_Final_Project.Models;
using ITI_Tanta_Final_Project.Repositories.Contracts;
using ITI_Tanta_Final_Project.context;

namespace ITI_Tanta_Final_Project.Repositories.Implementations
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly Context _context;
        private readonly DbSet<Course> _db;

        public CourseRepository(Context ctx) : base(ctx)
        {
            _context = ctx;
            _db = ctx.Set<Course>();
        }

        public Task<Course?> GetByNameAsync(string name) =>
            _db.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);

        public async Task<IEnumerable<Course>> SearchAsync(string? name, string? category)
        {
            var q = _db.AsNoTracking().Include(c => c.Instructor).AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                q = q.Where(c => c.Name.Contains(name));
            if (!string.IsNullOrWhiteSpace(category))
                q = q.Where(c => c.Category.Contains(category));
            return await q.ToListAsync();
        }
    }
}
