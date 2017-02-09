using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{

    public class EmployeeViewModel //lab7
    {
        public string EmployeeName { get; set; }
        public string Salary { get; set; }
        public string SalaryColour { get; set; }
    }

    public class EmployeeListViewModel:BaseViewModel //inheritance lab25
    {
        public List<EmployeeViewModel> Employees { get; set; }
    }
}