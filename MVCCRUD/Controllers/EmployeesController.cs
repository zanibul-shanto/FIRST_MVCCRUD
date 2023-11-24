using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCRUD.Data;
using MVCCRUD.Models;
using MVCCRUD.Models.DomainModels;

namespace MVCCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CrudDBContext crudDBContext;

        public EmployeesController(CrudDBContext  _CrudDBContext)
        {
            crudDBContext = _CrudDBContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task <IActionResult> Add(AddEmployeeViewModel addEmployee)
        {
            var employee = new Employee()
            {

                Id = Guid.NewGuid(),
                Name = addEmployee.Name,
                Email = addEmployee.Email,
                Salary = addEmployee.Salary,
                Department = addEmployee.Department
            };

            await crudDBContext.Employees.AddAsync(employee);
            await crudDBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var employees = await crudDBContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await crudDBContext.Employees.FirstOrDefaultAsync (x => x.Id == id);   

            if (employee != null)
            {
                var viewModel = new Employee()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department
                     
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");   
        }

        [HttpPost]

        public async Task<IActionResult> View(Employee model)
        {
            var employee = await crudDBContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;   
                employee.Salary = model.Salary;
                employee.Department = model.Department;

                await crudDBContext.SaveChangesAsync();

                return RedirectToAction("View");
            }

            return RedirectToAction("View");

        }

        [HttpPost]

        public async Task<IActionResult> Delete (Employee model)
        {
            var employee = await crudDBContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                crudDBContext.Employees.Remove(employee);
                await crudDBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


    }

}
