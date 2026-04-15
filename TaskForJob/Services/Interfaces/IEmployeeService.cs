using Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace TaskForJob.Services.Interfaces
{
    public interface IEmployeeService
    {
        public List<Employees> Import(IFormFile file);
        public bool Editer(Employees model);
    }
}
