using ITI_Tanta_Final_Project.Models;
using ITI_Tanta_Final_Project.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITI_Tanta_Final_Project.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork _uow;

        public CoursesController(IUnitOfWork uow) => _uow = uow;

        public async Task<IActionResult> Index(string? search)
        {
            var items = await _uow.Courses.SearchAsync(search);

            ViewBag.CurrentFilter = search;
            return View(items);
        }


        public async Task<IActionResult> Details(int id)
        {
            var course = await _uow.Courses.GetByIdAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        private async Task PopulateInstructorsDropDown(object? selected = null)
        {
            var instructors = await _uow.Users.GetUsersByRoleAsync(ITI_Tanta_Final_Project.Models.User.Role.Instructor);
            ViewBag.Instructors = new SelectList(instructors, "Id", "Name", selected);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateInstructorsDropDown();
            return View(new Course());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course model)
        {
            var byName = await _uow.Courses.GetByNameAsync(model.Name);
            if (byName != null)
                ModelState.AddModelError(nameof(model.Name), "Course name must be unique.");

            if (!ModelState.IsValid)
            {
                await PopulateInstructorsDropDown(model.InstructorId);
                return View(model);
            }

            model.Instructor = null;

            await _uow.Courses.AddAsync(model);
            await _uow.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var course = await _uow.Courses.GetByIdAsync(id);
            if (course == null) return NotFound();
            await PopulateInstructorsDropDown(course.InstructorId);
            return View(course);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course model)
        {
            if (id != model.Id) return BadRequest();

            var existingName = await _uow.Courses.GetByNameAsync(model.Name);
            if (existingName != null && existingName.Id != model.Id)
                ModelState.AddModelError(nameof(model.Name), "Course name must be unique.");

            if (!ModelState.IsValid)
            {
                await PopulateInstructorsDropDown(model.InstructorId);
                return View(model);
            }

            await _uow.Courses.UpdateAsync(model);
            await _uow.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _uow.Courses.GetByIdAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _uow.Courses.DeleteAsync(id);
            await _uow.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
