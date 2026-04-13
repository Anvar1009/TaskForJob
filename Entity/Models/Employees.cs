using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string Fore_Names { get; set; }
        public string Sur_Names { get; set; }
        public DateTime DataOfBirth  { get; set; }
        public string Telephone { get; set; }
        public string Mobile_phone { get; set; }
        public string Address { get; set; }
        public string Address_2 { get; set; }
        public string Post_Code { get; set; }
        public string Email_Home { get; set; }
        public DateTime StartDate { get; set; }   

    }
}
