using ITI_Tanta_Final_Project.Models;
using ITI_Tanta_Final_Project.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITI_Tanta_Final_Project.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _uow;

        public UsersController(IUnitOfWork uow) => _uow = uow;

        public async Task<IActionResult> Index(User.Role? role)
        {
            var users = await _uow.Users.GetAllAsync();
            if (role.HasValue)
                users = users.Where(u => u.UserRole == role.Value);

            return View(users);
        }

        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(Enum.GetValues(typeof(User.Role)));
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {

            var existing = await _uow.Users.GetByEmailAsync(user.Email);
            if (existing != null)
                ModelState.AddModelError(nameof(user.Email), "Email already exists.");

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(Enum.GetValues(typeof(User.Role)));
                return View(user);
            }

            await _uow.Users.AddAsync(user);
            await _uow.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user == null) return NotFound();

            ViewBag.Roles = new SelectList(Enum.GetValues(typeof(User.Role)));
            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User model)
        {
            if (id != model.Id) return BadRequest();

            var existingUser = await _uow.Users.GetByIdAsync(id);
            if (existingUser == null) return NotFound();


            var emailUser = await _uow.Users.GetByEmailAsync(model.Email);
            if (emailUser != null && emailUser.Id != model.Id)
                ModelState.AddModelError(nameof(model.Email), "Email already exists.");

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(Enum.GetValues(typeof(User.Role)));
                return View(model);
            }


            existingUser.Name = model.Name;
            existingUser.Email = model.Email;
            existingUser.UserRole = model.UserRole;

            await _uow.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }
       
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(User user)
        {
            if (user == null) return BadRequest();

            var dbUser = await _uow.Users.GetById_include_teachingcoursesAsync(user.Id);
            if (dbUser == null) return NotFound();

            bool isInstructor = dbUser.UserRole == ITI_Tanta_Final_Project.Models.User.Role.Instructor;
            bool teachesCourses = dbUser.TeachingCourses != null && dbUser.TeachingCourses.Any();

            if (isInstructor && dbUser.TeachingCourses.Any())
            {
                TempData["DeleteError"] = "❌ Cannot delete instructor. They are assigned to one or more courses.";
                return RedirectToAction(nameof(Index));
            }


            await _uow.Users.DeleteAsync(dbUser);
            await _uow.CompleteAsync();

            TempData["DeleteSuccess"] = "✅ User deleted successfully.";
            return RedirectToAction(nameof(Index));
        }


       
    }
}
