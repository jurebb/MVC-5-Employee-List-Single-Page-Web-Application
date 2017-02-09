using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;          //lab35
using WebApplication1.Models;           //lab34
using WebApplication1.ViewModels.SPA;
using OldViewModel = WebApplication1.ViewModels;    //OldViewModel is an alias  //required because both namesapces WebApplication1.ViewModels.SPA and WebApplication1.ViewModels have classes with same name

namespace WebApplication1.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        // GET: SPA/Main    
        public ActionResult Index()         //lab33
        {
            MainViewModel v = new MainViewModel();
            v.UserName = User.Identity.Name;
            v.FooterData = new OldViewModel.FooterViewModel();
            v.FooterData.CompanyName = "StepByStepSchools|Jure Baban";     //can be set to dynamic value
            v.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index", v);
        }

        public ActionResult EmployeeList()      //lab34
        {
            EmployeeListViewModel _elvm = new EmployeeListViewModel();
            EmployeeBusinessLayer _ebl = new EmployeeBusinessLayer();
            List<Employee> _Le = _ebl.GetEmployees();
            List<EmployeeViewModel> _Levm = new List<EmployeeViewModel>();
            foreach(Employee e in _Le)
            {
                EmployeeViewModel _evm = new EmployeeViewModel();
                _evm.EmployeeName = e.FirstName + " " + e.LastName;
                _evm.Salary = e.Salary.Value.ToString("C");                           //ako ne radi makni "C"
                if(e.Salary > 15000)
                {
                    _evm.SalaryColour = "yellow";
                }
                else
                {
                    _evm.SalaryColour = "green";
                }
                _Levm.Add(_evm);
            }

            _elvm.Employees = _Levm;
            return View("EmployeeList",_elvm);
        }

        public ActionResult GetAddNewLink()        //lab34
        {
            if(Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

        [AdminFilter]                               //WebApplication1/Filters/..
        public ActionResult AddNew()                //lab35 (create employee dialog)
        {
            CreateEmployeeViewModel cevm = new CreateEmployeeViewModel();
            return PartialView("CreateEmployee", cevm);
        }

        [AdminFilter]
        public ActionResult SaveEmployee(Employee e)          //lab35 //In this approach MVC action method will return single EmployeeViewModel instead of EmployeeListViewModel (see EmployeeList act.mth.) which will be received at JavaScript end and using JavaScript new row will be inserted into the gridmanually. 
        {
            EmployeeBusinessLayer ebl = new EmployeeBusinessLayer();
            ebl.SaveEmployee(e);

            EmployeeViewModel evm = new EmployeeViewModel();
            evm.EmployeeName = e.FirstName + " " + e.LastName;
            evm.Salary = e.Salary.Value.ToString("C");
            if (e.Salary > 15000)
            {
                evm.SalaryColour = "yellow";
            }
            else
            {
                evm.SalaryColour = "green";
            }
            return Json(evm);                                       //!!!   JSON    !!!
        }
    }
}