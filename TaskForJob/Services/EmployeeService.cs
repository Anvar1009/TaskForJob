using Entity;
using Entity.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskForJob.Services.Interfaces;

namespace TaskForJob.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EntityContext _entityContext;
        public EmployeeService(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }
        public bool Editer(Employees model)
        {
            var emp = _entityContext.employees.Find(model.Id);

            if (emp == null)
            {
                return false;
            }
            model.DataOfBirth = DateTime.SpecifyKind(model.DataOfBirth, DateTimeKind.Utc);
            model.StartDate = DateTime.SpecifyKind(model.StartDate, DateTimeKind.Utc);

            _entityContext.employees.Update(model);
            _entityContext.SaveChanges();
            
            return true;
        }

        public List<Employees> Import(IFormFile file)
        {
            var employees = new List<Employees>();
            var employ = new Employees();

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
                            if (value_part.Length < 11) // If for length
                                continue;
                            if (string.IsNullOrWhiteSpace(value_part[0]) && string.IsNullOrWhiteSpace(value_part[1]))
                                continue; // If for first and second values null or space
                            DateTime.TryParseExact(
                                value_part[3],
                                "dd/MM/yyyy",
                                null,
                                System.Globalization.DateTimeStyles.None,
                                out DateTime dob
                            ); // data time version exact

                            DateTime.TryParseExact(
                                value_part[10],
                                "dd/MM/yyyy",
                                null,
                                System.Globalization.DateTimeStyles.None,
                                out DateTime startDate
                            );
                            employ = new Employees
                            {
                                Payroll_Number = value_part[0],
                                Fore_Names = value_part[1],
                                Sur_Names = value_part[2],
                                DataOfBirth = dob,
                                Telephone = value_part[4],
                                Mobile_phone = value_part[5],
                                Address = value_part[6],
                                Address_2 = value_part[7],
                                Post_Code = value_part[8],
                                Email_Home = value_part[9],
                                StartDate = startDate,
                            }; // mapping

                            employees.Add(employ);    // list add
                        }
                    }
                }

            // DB add
            if (!_entityContext.employees.Any(a => a.Payroll_Number == employ.Payroll_Number))
            {
                _entityContext.employees.AddRange(employees);
                _entityContext.SaveChanges();
            }
            

            return employees;
        }
    }
}
