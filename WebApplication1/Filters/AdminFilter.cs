using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filters
{
    //AdminFilter restricts access to action method by Non-Admin user
    public class AdminFilter: ActionFilterAttribute //represents the base class for filter attributes //Upgrade simple AdminFilter class to ActionFilter by inheriting it from ActionFilterAttribute class
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(!Convert.ToBoolean(filterContext.HttpContext.Session["IsAdmin"])) //ako nije admin
            {
                filterContext.Result = new ContentResult()   //gets or sets result that is returned by the action method
                {
                    Content="Unauthorized to access specific resource."
                };
            }
        }
    }
}