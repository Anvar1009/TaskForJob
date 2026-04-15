using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TaskForJob.Services;

namespace TaskForJob.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void Import_Add_Employees()
        {
            // created DB in memory
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new EntityContext(options);

            var service = new EmployeeService(context);
            // created face csv model
            var csvData = "Payroll,Name,Surname,DOB,Tel,Mobile,Addr1,Addr2,Post,Email,Start\n" +
                          "1,Anvar,Soxibov,01/01/2000,123,456,Tashkent,,100000,Ann@mail.com,01/01/2020\n" +
                          "2,Davron,Choriyev,02/02/2001,123,456,Tashkent,,100000,Davron@mail.com,02/02/2021";

            var file = CreateFile(csvData);

            var result = service.Import(file);

            Assert.Equal(2, result.Count);
            Assert.Equal(2, context.employees.Count());
        }
        private IFormFile CreateFile(string content)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(content);
            var stream = new MemoryStream(bytes);

            return new FormFile(stream, 0, bytes.Length, "Data", "test.csv");
        }
        [Fact]
        public void Import_Return_Zero_When_File_Empty()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase(databaseName: "TestDb2")
                .Options;

            using var context = new EntityContext(options);
            var service = new EmployeeService(context);

            var file = CreateFile("Header1,Header2");

            var result = service.Import(file);

            Assert.Empty(result);
        }
    }
}
