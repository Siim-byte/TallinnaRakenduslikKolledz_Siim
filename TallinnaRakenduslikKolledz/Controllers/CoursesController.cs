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
            ViewData["action"] = "Create";
            PopulateDepartmentsDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Credits,Department,DepartmentID,Enrollments,CourseAssignments")]Course courses)
        {
            ViewData["action"] = "Create";
            if (ModelState.IsValid)
            {
                 _context.Courses.Add(courses);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
                //PopulateDepartmentsDropDownList(course.DepartmentID);
            }
            return View(courses);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var courses = await _context.Courses.FirstOrDefaultAsync(x => x.ID == id);
            if (courses == null)
            {
                return NotFound();
            }
            ViewData["action"] = "Edit";
            return View("Create", courses);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(int id, [Bind("ID,Title,Credits,Department,DepartmentID,Enrollments,CourseAssignments")] Course courses)
        {
            if (!ModelState.IsValid)
            {
                _context.Courses.Update(courses);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(courses);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var courses = await _context.Courses.FirstOrDefaultAsync(m => m.ID == id);
            if (courses == null)
            {
                return NotFound();
            }
            ViewBag.action = "Delete";
            return View("Delete", courses);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var courses = await _context.Courses.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }
            _context.Courses.Remove(courses);
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
            var courses = await _context.Courses.FirstOrDefaultAsync(m => m.ID == id);
            if (courses == null)
            {
                return NotFound();
            }
            ViewBag.action = "Details";
            return View("Delete", courses);
        }
    }
}