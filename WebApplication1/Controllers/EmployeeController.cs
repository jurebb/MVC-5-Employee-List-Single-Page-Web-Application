using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using WebApplication1.Filters; //lab23

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {

        [Authorize]
        [HeaderFooterFilter]//lab26
        public ActionResult Index() 
        {
            EmployeeListViewModel _elvm = new EmployeeListViewModel();


            EmployeeBusinessLayer ebl = new EmployeeBusinessLayer();
            List<Employee> employees = ebl.GetEmployees();

            List<EmployeeViewModel> evmList = new List<EmployeeViewModel>();

            foreach (Employee item in employees)
            {
                EmployeeViewModel evm = new EmployeeViewModel();
                evm.EmployeeName = item.FirstName + " " + item.LastName;
                evm.Salary = item.Salary.Value.ToString("C");
                //evm.Salary = item.Salary.ToString("C"); //NECE OD LAB 15 ????
                if (item.Salary>15000)
                {
                    evm.SalaryColour = "yellow";
                }
                else
                {
                    evm.SalaryColour = "green";
                }

                evmList.Add(evm);
            }

            _elvm.Employees = evmList;
            //_elvm.CurrentUser = "Admin";

            return View("Index", _elvm);
        }

        [AdminFilter] //using WebApplication1.Filters; //lab23.2
        [HeaderFooterFilter] //lab26
        public ActionResult AddNew()
        {
            //
            CreateEmployeeViewModel _el = new CreateEmployeeViewModel();
            //

            //return View("CreateEmployee", new CreateEmployeeViewModel());
            return View("CreateEmployee", _el);
        }

        [ValidateAntiForgeryToken]  //lab24 handle csrf attack
        [AdminFilter] //using WebApplication1.Filters; //lab23.2
        [HeaderFooterFilter] //lab26
        public ActionResult SaveEmployee(Employee e, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                    {
                        EmployeeBusinessLayer empBal = new EmployeeBusinessLayer(); //EmployeeBusinessLayer - za SaveEmployee ili GetEmployee
                        empBal.SaveEmployee(e);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel(); //CreateEmployeeViewModel klasa sa prop. FirstName, LastName, Salary
                        vm.FirstName = e.FirstName;                                 //da se repopulira ekran ako nije valid
                        vm.LastName = e.LastName;

                        //
                        //

                        if (e.Salary.HasValue)
                        {
                            vm.Salary = e.Salary.ToString();
                        }
                        else
                        {
                            vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                        }
                        return View("CreateEmployee", vm); // Day 4 Change - Passing e here
                    }
                case "Cancel":
                    return RedirectToAction("Index");
            }
            return new EmptyResult();
        }


        [ChildActionOnly] //Can we invoke GetAddNewLink directly via browser Address bar? to stop direct execution of GetAddNewLink.. //lab23talk
        public ActionResult GetAddNewLink()        //lab23
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}