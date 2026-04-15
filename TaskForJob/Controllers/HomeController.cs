using Entity;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TaskForJob.Models;
using TaskForJob.Services;
using TaskForJob.Services.Interfaces;

namespace TaskForJob.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly EntityContext _entityContext;
        public HomeController(EntityContext entityContext, EmployeeService employeeService)
        {
            _entityContext = entityContext;
            _employeeService = employeeService;
        }

        public  IActionResult Index()
        {
            var list =  _entityContext.employees.ToList();
            return View(list);
        }
        /// <summary>
        /// Action for Import File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Message"] = "Please select a valid file";
                return RedirectToAction("Index");
            }
            var emp = _employeeService.Import(file);
            TempData["Message"] = $"{emp.Count} rows successfully imported";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _entityContext.employees.Find(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }
        [HttpPost]
        public IActionResult Edit(Employees model)
        {
            if (ModelState.IsValid)
            {
                if (!ModelState.IsValid)
                    return View(model);

                var result = _employeeService.Editer(model);

                if (!result)
                    return NotFound();

                TempData["Message"] = "Employee updated successfully";

                return RedirectToAction("Index");
            }

            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
