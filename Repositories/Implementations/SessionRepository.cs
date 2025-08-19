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
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        private readonly Context _context;

        public SessionRepository(Context ctx) : base(ctx)
        {
            _context = ctx;
        }

        public Task<IEnumerable<Session>> GetByCourseNameAsync(string courseName) =>
            _context.Sessions
               .AsNoTracking()
               .Include(s => s.Course)
               .Where(s => s.Course.Name.Contains(courseName))
               .ToListAsync()
               .ContinueWith(t => t.Result.AsEnumerable());
    }
}
