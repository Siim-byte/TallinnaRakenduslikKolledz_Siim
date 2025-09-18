using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikKolledz.Data;
using TallinnaRakenduslikKolledz.Models;

namespace TallinnaRakenduslikKolledz.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly SchoolContext _context;
        public InstructorsController(SchoolContext context)
        {
            _context = context;   
        }
        public async Task<IActionResult> Index(int? Id, int? courseId)
        {
            var vm = new InstructorIndexData();
            vm.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ToListAsync();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var instructor = new Instructor();
            instructor.CourseAssignments = new List<CourseAssignment>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor, int[] selectedCourses)
        {
            instructor.CourseAssignments = new List<CourseAssignment>();

            if (selectedCourses != null)
            {
                foreach (var courseId in selectedCourses)
                {
                    instructor.CourseAssignments.Add(new CourseAssignment
                    {
                        CourseID = courseId,
                        InstructorID = instructor.ID
                    });
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deleteableInstructor = await _context.Instructors
                .FirstOrDefaultAsync(s => s.ID == id);
            if (deleteableInstructor == null)
            {
                return NotFound();
            }
            return View(deleteableInstructor);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Instructor deletableInstructor = await _context.Instructors
                .SingleAsync(i  => i.ID == id);
            _context.Instructors.Remove(deletableInstructor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = _context.Courses; //leiame koik kursused

            var instructorCourses = new HashSet<int>(instructor.CourseAssignments.Select(c => c.CourseID));
            // valime kursused kus courseid on opetajal olemas
            var vm = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                vm.Add(new AssignedCourseData()
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewData["Courses"] = vm;

        }
    }
}
