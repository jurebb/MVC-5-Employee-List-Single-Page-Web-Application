using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;
using WebApplication1.ViewModels;
using System.IO;
using WebApplication1.Models;
using System.Threading; //lab28
using System.Threading.Tasks; //lab28

namespace WebApplication1.Controllers
{
    public class BulkUploadController : AsyncController     //lab27
    {
        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }

        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
            List<Employee> employees = new List<Employee>();
            StreamReader csvreader = new StreamReader(model.fileUpload.InputStream);
            csvreader.ReadLine(); // Assuming first line is header
            while (!csvreader.EndOfStream)
            {
                var line = csvreader.ReadLine();
                var values = line.Split(',');//Values are comma separated
                Employee e = new Employee();
                e.FirstName = values[0];
                e.LastName = values[1];
                e.Salary = int.Parse(values[2]);
                employees.Add(e);
            }
            return employees;
        }

        [AdminFilter]
        [HandleError]   //radi na lokalnom, ne radi na globalnom nivou, (iskomentirana linija u App_start/FilterConfig.cs, lab29)
        //[EmployeeExceptionFilter]       //lab30, za logganje excepcija, globalno radi u (App_start/FilterConfig.cs, lab30), pa je ovdje iskomentirano
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            int t1 = Thread.CurrentThread.ManagedThreadId;      //za vidit da su t1 i t2 razliciti threadovi (breakpoint)
            List<Employee> employees = await Task.Factory.StartNew<List<Employee>>
                (() => GetEmployees(model));
            int t2 = Thread.CurrentThread.ManagedThreadId;      //za vidit da su t1 i t2 razliciti threadovi (breakpoint)
            EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
            bal.UploadEmployees(employees);
            return RedirectToAction("Index", "Employee");
        }
    }
}