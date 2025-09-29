using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledz.Data;
using TallinnaRakenduslikKolledz.Models;
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
        public async Task<IActionResult> Create([Bind("DepartmentID, Name, Budget, StartDate,RowVersion, Administrator, PhoneNumber, EndDate, IsDeleted ")] Department departments)
        {
            ViewData["action"] = "Delete";
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
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["action"] = "Delete";
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Departments
                .Include(d => d.Administrator)
                .FirstOrDefaultAsync(d => d.DepartmentID == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Department department)
        {
            ViewData["action"] = "Delete";
            if (await _context.Departments.AnyAsync(m => m.DepartmentID == department.DepartmentID))
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["action"] = "Details";
            if (id == null) { return NotFound(); }

            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentID == id);
            return View(nameof(Delete), department);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["action"] = "Edit";
            if (id == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.DepartmentID == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed([Bind("ID,LastName,FirstName,HiredDate,OfficeAssignment,Salary,RoomId,Email,")] Department department)
        {
            ViewData["action"] = "Edit";
            if (ModelState.IsValid)
            {
                _context.Departments.Update(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}