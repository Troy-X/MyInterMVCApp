using Microsoft.AspNetCore.Mvc;
using MyInterMVCApp.Models;
using MyInterMVCApp.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyInterMVCApp.ViewModels;

namespace MyInterMVCApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var courses = _context.Courses.AsNoTracking().ToList();
            return View(courses);
        }

        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(CourseViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var course = new Course()
            {
                Title = model.Title,
                Code = model.Code,
                Units = model.Units,
            };

            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();

            var model = new CourseViewModel()
            {
                Id = id,
                Title = course.Title,
                Code = course.Code,
                Units = course.Units,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditCourse(CourseViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var course = _context.Courses.Find(model.Id);
            if (course == null) return NotFound();

            course.Title = model.Title;
            course.Code = model.Code;
            course.Units = model.Units;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCourse(int id)
        {
            var courses = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (courses != null)
            {
                _context.Courses.Remove(courses);
                _context.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }
    }
}
