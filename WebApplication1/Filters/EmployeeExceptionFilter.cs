using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;           //lab30, za : HandleErrorAttribute
using WebApplication1.Logger;   //lab30, za FileLogger

namespace WebApplication1.Filters
{
    public class EmployeeExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            FileLogger fileL = new FileLogger();
            fileL.LogException(filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}