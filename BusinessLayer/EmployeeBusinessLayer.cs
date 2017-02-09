using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DataAccessLayer; //!

namespace WebApplication1.Models
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            SalesERPDAL salesDal = new SalesERPDAL(); //SalesERPDAL sadrzi DbSet - DbSet (Employees) will represent all the employees that can be queried from the database.
            return salesDal.Employees.ToList();
        }

        public Employee SaveEmployee(Employee e)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();
            return e;
        }

        /**public bool IsValidUser(UserDetails u)               //valid till lab23
        {
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
            /**In business layer we are comparing username and password with hardcoded values. 
            In real time we can make call to Database layer and compare it with real time values.
        }**/ 

        public UserStatus GetUserValidity(UserDetails u)   //from lab23 onw
        {
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return UserStatus.AuthenticatedAdmin; //enum
            }
            else if (u.UserName == "Jure" && u.Password == "Jure")
            {
                return UserStatus.AuthenticatdUser;   //enum
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }

        public void UploadEmployees(List<Employee> employees)   //lab27
        {
            SalesERPDAL sales = new SalesERPDAL();
            sales.Employees.AddRange(employees);
            sales.SaveChanges();
        }
    }
}