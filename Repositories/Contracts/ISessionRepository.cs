using ITI_Tanta_Final_Project.Models;

namespace ITI_Tanta_Final_Project.Repositories.Contracts
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task<IEnumerable<Session>> GetAllWithCoursesAsync();
        Task<IEnumerable<Session>> GetByCourseNameAsync(string courseName);
        Task<IEnumerable<Session>> SearchAsync(string? search);

    }
}
