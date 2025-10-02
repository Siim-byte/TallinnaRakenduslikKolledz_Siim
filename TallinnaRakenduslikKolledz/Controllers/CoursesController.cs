using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledz.Data;
using TallinnaRakenduslikKolledz.Models;

namespace TallinnaRakenduslikKolllež.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var courses = _context.Courses.Include(c => c.Department)
                .AsNoTracking();
            Console.WriteLine(courses);
            return View(courses);
        }
        [HttpGet]
        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                 _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                //PopulateDepartmentsDropDownList(course.DepartmentID);
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.action = "Delete";
            return View("Delete", course);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Departments
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentID", "Name", selectedDepartment);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.action = "Details";
            return View("Delete", course);
        }
    }
}