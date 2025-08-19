using ITI_Tanta_Final_Project.Models;

namespace ITI_Tanta_Final_Project.Repositories.Contracts
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course?> GetByNameAsync(string name);
        Task<IEnumerable<Course>> SearchAsync(string? name, string? category);
    }

}
