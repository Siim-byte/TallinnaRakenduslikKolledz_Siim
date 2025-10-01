using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledz.Data;
using TallinnaRakenduslikKolledz.Models;

namespace TallinnaRakenduslikKolllež.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SchoolContext _context;

        public DepartmentsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Departments.Include(d => d.Administrator);
            return View(await schoolContext.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["action"] = "Create";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Budget,StartDate,RowVersion,Administrator,Quota,LeaderName,Pet")] Department departments)
        {
            ViewData["action"] = "Create";
            if (ModelState.IsValid)
            {
                _context.Departments.Add(departments);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
                // return RedirectToAction(nameof(Index))
            }
            return View(departments);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentID == id);
            if (department == null)
            {
                return NotFound();
            }
            ViewData["action"] = "Edit";
            return View("Create", department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(int id, [Bind("DepartmentID,Name,Budget,StartDate,RowVersion,Administrator,Quota,LeaderName,Pet")] Department department)
        {
            ViewData["action"] = "Edit";
            if (ModelState.IsValid)
            {
                var existing = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentID == id);
                _context.Departments.Update(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["action"] = "Edit";
            return View("Create", department);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (department == null)
            {
                return NotFound();
            }
            ViewBag.action = "Delete";
            return View("Delete", department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (department == null)
            {
                return NotFound();
            }
            ViewBag.action = "Details";
            return View("Delete", department);
        }
    }
}