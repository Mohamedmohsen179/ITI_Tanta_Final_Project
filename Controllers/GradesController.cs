using ITI_Tanta_Final_Project.Models;
using ITI_Tanta_Final_Project.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITI_Tanta_Final_Project.Controllers
{
    public class GradesController : Controller
    {
        private readonly IUnitOfWork _uow;

        public GradesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Grades
        public async Task<IActionResult> Index(string? search)
        {
            var grades = await _uow.Grades.GetAllWithDetailsAsync();
            if (!string.IsNullOrWhiteSpace(search))
            {
                grades = await _uow.Grades.SearchAsync(search);
            }
            return View(grades);
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var grade = await _uow.Grades.GetByIdAsync(id);
            if (grade == null) return NotFound();
            return View(grade);
        }

        // GET: Grades/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View(new Grade());
        }

        // POST: Grades/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Grade model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns();
                return View(model);
            }

            await _uow.Grades.AddAsync(model);
            await _uow.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Grades/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var grade = await _uow.Grades.GetByIdAsync(id);
            if (grade == null) return NotFound();

            await PopulateDropdowns(grade.SessionId, grade.TraineeId);
            return View(grade);
        }

        // POST: Grades/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Grade model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(model.SessionId, model.TraineeId);
                return View(model);
            }

            var grade = await _uow.Grades.GetByIdAsync(id);
            if (grade == null) return NotFound();

            grade.SessionId = model.SessionId;
            grade.TraineeId = model.TraineeId;
            grade.value = model.value;

            await _uow.Grades.UpdateAsync(grade);
            await _uow.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _uow.Grades.GetByIdWithDetailsAsync(id);
            if (grade == null) return NotFound();
            return View(grade);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _uow.Grades.GetByIdWithDetailsAsync(id); 
            if (grade == null) return NotFound();

            try
            {
                await _uow.Grades.DeleteAsync(grade);
                await _uow.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete grade. It may be referenced by other records.");
                return View(grade);
            }
        }

        private async Task PopulateDropdowns(int? selectedSessionId = null, int? selectedTraineeId = null)
        {
            var sessions = await _uow.Sessions.GetAllAsync();
            var trainees = (await _uow.Users.GetAllAsync())
                            .Where(u => u.UserRole == ITI_Tanta_Final_Project.Models.User.Role.Trainee);

            ViewBag.Sessions = new SelectList(sessions, "Id", "Title", selectedSessionId);
            ViewBag.Trainees = new SelectList(trainees, "Id", "Name", selectedTraineeId);
        }
    }
}
