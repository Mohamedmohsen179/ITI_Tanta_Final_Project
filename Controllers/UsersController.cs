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
            ViewBag.Roles = new SelectList(System.Enum.GetValues(typeof(User.Role)));
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            // Unique Email
            var existing = await _uow.Users.GetByEmailAsync(user.Email);
            if (existing != null)
                ModelState.AddModelError(nameof(user.Email), "Email already exists.");

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(System.Enum.GetValues(typeof(User.Role)));
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

            ViewBag.Roles = new SelectList(System.Enum.GetValues(typeof(User.Role)));
            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User model)
        {
            if (id != model.Id) return BadRequest();

            var existing = await _uow.Users.GetByEmailAsync(model.Email);
            if (existing != null && existing.Id != model.Id)
                ModelState.AddModelError(nameof(model.Email), "Email already exists.");

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(System.Enum.GetValues(typeof(User.Role)));
                return View(model);
            }

            await _uow.Users.UpdateAsync(model);
            await _uow.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user == null) return NotFound();

            await _uow.Users.DeleteAsync(id);
            await _uow.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
