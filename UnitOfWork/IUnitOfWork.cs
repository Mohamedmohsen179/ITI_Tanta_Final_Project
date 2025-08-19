using ITI_Tanta_Final_Project.Repositories.Contracts;

namespace ITI_Tanta_Final_Project.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IGradeRepository Grades { get; }
        ISessionRepository Sessions { get; }
        ICourseRepository Courses { get; }

        Task<int> CompleteAsync();
    }
}
