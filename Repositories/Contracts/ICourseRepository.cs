using ITI_Tanta_Final_Project.Models;

namespace ITI_Tanta_Final_Project.Repositories.Contracts
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course?> GetByNameAsync(string name);
        Task<IEnumerable<Course>> SearchAsync(string? search);
        Task<IEnumerable<Course>> GetAllSessionsAsync(string? search);
        Task<Course?> GetByIdWithInstructorAsync(int id);
        Task<Course?> GetByIdWithSessionAsync(int id);
        Task<Course?> GetDetails(int id);


    }

}
