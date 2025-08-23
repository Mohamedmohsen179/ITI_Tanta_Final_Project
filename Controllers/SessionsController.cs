using ITI_Tanta_Final_Project.Models;
using ITI_Tanta_Final_Project.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ITI_Tanta_Final_Project.Controllers
{
    public class SessionsController : Controller
    {
        private readonly IUnitOfWork _uow;

        public SessionsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _uow.Sessions.GetAllAsync();

            return View(sessions);
        }
        public async Task<IActionResult> Details(int id)
        {
            var session = await _uow.Sessions.GetByIdAsync(id);
            if (session == null) return NotFound();
            return View(session);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Courses = await _uow.Courses.GetAllAsync();
            return View(new Session());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Session model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = await _uow.Courses.GetAllAsync();
                return View(model);
            }
            await _uow.Sessions.AddAsync(model);
            await _uow.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _uow.Sessions.GetByIdAsync(id);
            if (session == null) return NotFound();
            ViewBag.Courses = await _uow.Courses.GetAllAsync();
            return View(session);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Session model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = await _uow.Courses.GetAllAsync();
                return View(model);
            }
            var session = await _uow.Sessions.GetByIdAsync(id);
            if (session == null) return NotFound();
            session.Title = model.Title;
            session.StartingTime = model.StartingTime;
            session.EndingTime = model.EndingTime;
            session.CourseId = model.CourseId;
            await _uow.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _uow.Sessions.GetByIdAsync(id);
            if (session == null) return NotFound();
            return View(session);
        }
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Session session)
        {
            if (session == null) return NotFound();
            try
            {
                await _uow.Sessions.DeleteAsync(session);
                await _uow.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete session. It may be referenced by other records.");
                return View(session);
            }
        }
    }

}





