using Microsoft.EntityFrameworkCore;
using ITI_Tanta_Final_Project.Models;
using ITI_Tanta_Final_Project.Repositories.Contracts;
using ITI_Tanta_Final_Project.context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI_Tanta_Final_Project.Repositories.Implementations
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        private readonly Context _context;
        private readonly DbSet<Grade> _db;

        public GradeRepository(Context ctx) : base(ctx)
        {
            _context = ctx;
            _db = _context.Set<Grade>();
        }

        public async Task<IEnumerable<Grade>> GetByTraineeAsync(int traineeId) =>
            await _db.AsNoTracking()
                     .Include(g => g.Session)
                        .ThenInclude(s => s.Course)
                     .Where(g => g.TraineeId == traineeId)
                     .ToListAsync();

        public async Task<IEnumerable<Grade>> GetBySessionAsync(int sessionId) =>
            await _db.AsNoTracking()
                     .Include(g => g.Trainee)
                     .Where(g => g.SessionId == sessionId)
                     .ToListAsync();

        public async Task RecordGradeAsync(int traineeId, int sessionId, int value)
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 100");

            var grade = await _db.FirstOrDefaultAsync(
                g => g.TraineeId == traineeId && g.SessionId == sessionId);

            if (grade == null)
            {
                grade = new Grade
                {
                    TraineeId = traineeId,
                    SessionId = sessionId,
                    value = value
                };
                await _db.AddAsync(grade);
            }
            else
            {
                grade.value = value;
                _db.Update(grade);
            }

            await _context.SaveChangesAsync();
        }
    }
}
