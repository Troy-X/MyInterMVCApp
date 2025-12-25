using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyInterMVCApp.Data;
using MyInterMVCApp.Models;
using MyInterMVCApp.ViewModels;
using System.Linq;

namespace MyInterMVCApp.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly AppDbContext _context;
        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var departments = _context.Departments.AsNoTracking().ToList();
            return View(departments);
        }

        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDepartment(DepartmentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var department = new Department()
            {
                Name = model.Name,
                Location = model.Location
            };

            _context.Departments.Add(department);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditDepartment(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null) return NotFound();

            var model = new DepartmentViewModel()
            {
                Id = department.Id,
                Name = department.Name,
                Location = department.Location
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditDepartment(DepartmentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var department = _context.Departments.Find(model.Id);
            if (department == null) return NotFound();

            department.Name = model.Name;
            department.Location = model.Location;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteDepartment(int id)
        {
            var department = _context.Departments.Find(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
    }
}
