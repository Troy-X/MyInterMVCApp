using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyInterMVCApp.Data;
using MyInterMVCApp.Models;
using MyInterMVCApp.ViewModels;
using System.Linq;

namespace MyInterMVCApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchTerm, string clas, string sortOrder)
        {
            var students = _context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                students = students.Where(s => s.Name.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(clas))
            {
                students = students.Where(s => s.Class == clas);
            }

            students = sortOrder switch
            {
                "name_desc" => students.OrderByDescending(s => s.Name),
                "class_desc" => students.OrderByDescending(s => s.Class),
                _ => students.OrderBy(s => s.Name)
            };

            return View(students.ToList());
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(StudentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var student = new Student()
            {
                Name = model.Name,
                Class = model.Class
            };

            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult EditStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            var model = new StudentViewModel()
            {
                Id = student.Id,
                Name = student.Name,
                Class = student.Class
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditStudent(StudentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var student = _context.Students.Find(model.Id);
            if (student == null) return NotFound();

            student.Name = model.Name;
            student.Class = model.Class;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            var model = new StudentViewModel()
            {
                Id=student.Id,
                Name = student.Name,
                Class = student.Class
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStudentConfirmed(StudentViewModel model)
        {
            var student = _context.Students.Find(model.Id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
