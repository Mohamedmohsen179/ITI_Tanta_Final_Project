using ITI_Tanta_Final_Project.Models;

namespace ITI_Tanta_Final_Project.Repositories.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> GetById_include_teachingcoursesAsync(int id);
        Task<IEnumerable<User>> GetUsersByRoleAsync(User.Role role);
    }
}
