using ITI_Tanta_Final_Project.context;
using ITI_Tanta_Final_Project.Repositories.Contracts;
using ITI_Tanta_Final_Project.Repositories.Implementations;

namespace ITI_Tanta_Final_Project.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public IUserRepository Users { get; }
        public IGradeRepository Grades { get; }
        public ISessionRepository Sessions { get; }
        public ICourseRepository Courses { get; }

        public UnitOfWork(Context context)
        {
            _context = context;

            Users = new UserRepository(_context);
            Grades = new GradeRepository(_context);
            Sessions = new SessionRepository(_context);
            Courses = new CourseRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
