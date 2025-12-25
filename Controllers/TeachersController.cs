using Microsoft.AspNetCore.Mvc;
using MyInterMVCApp.Models;
using MyInterMVCApp.Data;
using System.Linq;

namespace MyInterMVCApp.Controllers
{
    public class TeachersController : Controller
    {
        private readonly AppDbContext _context;
        public TeachersController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var teachers = _context.Teachers.ToList();
            return View(teachers);
        }

        public IActionResult AddTeacher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTeacher(Teacher teacher)
        {
            var teachers = _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditTeacher(int id)
        {
            var teachers = _context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teachers == null) return NotFound();
            return View(teachers);
        }

        [HttpPost]
        public IActionResult EditTeacher(Teacher teacher)
        {
            var CurrentTeacher = _context.Teachers.FirstOrDefault(t => t.Id == teacher.Id);
            if (CurrentTeacher == null) return NotFound();

            
                CurrentTeacher.FullName = teacher.FullName;
                CurrentTeacher.Subject = teacher.Subject;
                CurrentTeacher.Email = teacher.Email;
            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTeacher(int id)
        {
            var teachers= _context.Teachers.FirstOrDefault(t =>t.Id == id);
            if (teachers != null)
            {
                _context.Teachers.Remove(teachers);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
