using Entity;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TaskForJob.Models;

namespace TaskForJob.Controllers
{
    public class HomeController : Controller
    {
        private readonly EntityContext _entityContext;
        public HomeController(EntityContext entityContext)
        {
            _entityContext = entityContext;
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

            var employees = new List<Employees>();
            if (file != null && file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    reader.ReadLine();  // skip header
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();// read one row
                        if (string.IsNullOrWhiteSpace(line)) // If for line null or space
                            continue;
                        var value_part = line.Split(','); // row to parts by ','
                        if (value_part.Length < 11 ) // If for length
                            continue;
                        if (string.IsNullOrWhiteSpace(value_part[0]) && string.IsNullOrWhiteSpace(value_part[1]))
                            continue; // If for first and second values null or space
                        DateTime.TryParseExact(
                            value_part[2],
                            "dd/MM/yyyy",
                            null,
                            System.Globalization.DateTimeStyles.None,
                            out DateTime dob
                        ); // data time version exact

                        DateTime.TryParseExact(
                            value_part[9],
                            "dd/MM/yyyy",
                            null,
                            System.Globalization.DateTimeStyles.None,
                            out DateTime startDate
                        );
                        var employ = new Employees
                        {
                            Fore_Names = value_part[1],
                            Sur_Names = value_part[2],
                            DataOfBirth =dob,
                            Telephone = value_part[4],
                            Mobile_phone = value_part[5],
                            Address = value_part[6],
                            Address_2 = value_part[7],
                            Post_Code = value_part[8],
                            Email_Home = value_part[9],
                            StartDate =startDate,
                        }; // mapping
                       
                        employees.Add( employ );    // list add
                    }
                }
            }
            // DB add
            _entityContext.employees.AddRange(employees);
            _entityContext.SaveChanges();
            ViewBag.Count = employees.Count; // send to count in view
            return View("Index", _entityContext.employees.ToList());
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
                try
                {

                    model.DataOfBirth = DateTime.SpecifyKind(model.DataOfBirth, DateTimeKind.Utc);
                    model.StartDate = DateTime.SpecifyKind(model.StartDate, DateTimeKind.Utc);

                    _entityContext.employees.Update(model);
                    _entityContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    var error = ex.InnerException?.Message;
                    throw;
                }

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
