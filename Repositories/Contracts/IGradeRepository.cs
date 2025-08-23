using ITI_Tanta_Final_Project.Models;

namespace ITI_Tanta_Final_Project.Repositories.Contracts
{
    public interface IGradeRepository : IRepository<Grade>
    {
        Task<IEnumerable<Grade>> GetByTraineeAsync(int traineeId);
        Task<IEnumerable<Grade>> GetBySessionAsync(int sessionId);
        Task RecordGradeAsync(int traineeId, int sessionId, int score);
        Task<IEnumerable<Grade>> GetAllWithDetailsAsync();
        Task<Grade?> GetByIdWithDetailsAsync(int id);
    }
}
