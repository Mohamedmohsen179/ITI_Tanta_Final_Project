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

        public async Task<Course?> GetByIdWithInstructorAsync(int id)
        {
            return await _db.Include(c => c.Instructor)
                            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Course>> SearchAsync(string? search)
        {
            var q = _db.AsNoTracking().Include(c => c.Instructor).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                q = q.Where(c =>
                    c.Name.Contains(search) ||
                    c.Category.Contains(search) ||
                    c.Instructor.Name.Contains(search));
            }

            return await q.ToListAsync();
        }
        public async Task<Course?> GetByIdWithSessionAsync(int id) 
        {
            return await _db.Include(c => c.Sessions)
                            .FirstOrDefaultAsync(c => c.Id == id);
        }


        public Task<IEnumerable<Course>> GetAllSessionsAsync(string? search)
        {
            var query = _db
                .Include(c => c.Sessions)
                .Include(c => c.Instructor)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c =>
                    c.Name.Contains(search) ||
                    c.Category.Contains(search) ||
                    c.Sessions.Any(s => s.Title.Contains(search)));
            }
            return query
                .ToListAsync()
                .ContinueWith(t => t.Result.AsEnumerable());
        }

        public Task<Course?> GetDetails(int id)
        {
            return _db
                .Include(c => c.Instructor)
                .Include(c => c.Sessions)
                .Include(c => c.Trainees)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
