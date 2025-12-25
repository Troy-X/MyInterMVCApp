using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyInterMVCApp.Data;
using MyInterMVCApp.Models;
using MyInterMVCApp.ViewModels;
using System.Linq;

namespace MyInterMVCApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var employees = _context.Employees.AsNoTracking().ToList();
            return View(employees);
        }

        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var employee = new Employee()
            {
                Name = model.Name,
                Position = model.Position,
                Salary = model.Salary
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
            
        }

        public IActionResult EditEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null) return NotFound();

            var model = new EmployeeViewModel()
            {
                Id = employee.Id,
                Name=employee.Name,
                Position = employee.Position,
                Salary = employee.Salary
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditEmployee(EmployeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var employee = _context.Employees.Find(model.Id);
                if (employee == null) return NotFound();

                employee.Name = model.Name;
                employee.Position = model.Position;
                employee.Salary = model.Salary;

                _context.SaveChanges();

                return RedirectToAction("Index");
            
            
            
        }

        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
