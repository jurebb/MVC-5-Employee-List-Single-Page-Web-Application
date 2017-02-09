using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;
using System.IO;
using WebApplication1.Models;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.ViewModels.SPA;

namespace WebApplication1.Areas.SPA.Controllers
{
    public class SpaBulkUploadController : AsyncController           //lab36        //!!! za async controllere opc.
    {
        // GET: SPA/SpaBulkUpload
        [AdminFilter]                                           //!!!
        public ActionResult Index()                             //lab36
        {
            return PartialView("Index");
        }

        [AdminFilter]                                                   //lab36         //!!!
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            int t1 = Thread.CurrentThread.ManagedThreadId;
            List<Employee> _employees = await Task.Factory.StartNew<List<Employee>>(() => GetEmployees(model));     //!!!
            int t2 = Thread.CurrentThread.ManagedThreadId;

            EmployeeBusinessLayer _ebl = new EmployeeBusinessLayer();
            _ebl.UploadEmployees(_employees);

            EmployeeListViewModel _elvm = new EmployeeListViewModel();
            _elvm.Employees = new List<EmployeeViewModel>();
            foreach(Employee _e in _employees)
            {
                EmployeeViewModel _evm = new EmployeeViewModel();
                _evm.EmployeeName = _e.FirstName + " " + _e.LastName;
                _evm.Salary = _e.Salary.Value.ToString("C");
                if(_e.Salary>15000)
                {
                    _evm.SalaryColour = "yellow";
                }
                else
                {
                    _evm.SalaryColour = "green";
                }
                _elvm.Employees.Add(_evm);
            }
            return Json(_elvm);
        }

        private List<Employee> GetEmployees(FileUploadViewModel model)      //lab36
        {
            List<Employee> _le = new List<Employee>();                          //list of emps read from file to return to main function above (Upload)
            StreamReader _sr = new StreamReader(model.fileUpload.InputStream);
            _sr.ReadLine();                                                     //assuming first line is header
            while(!_sr.EndOfStream)
            {
                var line = _sr.ReadLine();
                var values = line.Split(',');                                  //values are comma seperated
                Employee _e = new Employee();
                _e.FirstName = values[0];
                _e.LastName = values[1];
                _e.Salary = int.Parse(values[2]);
                _le.Add(_e);
            }
            return _le;
        }
    }
}