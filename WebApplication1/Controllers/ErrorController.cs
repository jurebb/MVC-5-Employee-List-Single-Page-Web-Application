using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    [AllowAnonymous]   //lab29,  because error controller and index action should not be bound to an authenticated user. It may be possible that user has entered invalid URL before login.
    public class ErrorController : Controller       //za handlanje "Resource not found" (cudni URL) ekscepcija, lab29
    {
        // GET: Error
        public ActionResult Index()
        {
            Exception e = new Exception("Invalid Action and/or Controller name");
            HandleErrorInfo eInfo = new HandleErrorInfo(e, "Unknown", "Unknown");   //HandleErrorInfo constructor takes 3 arguments – Exception object, controller nameand Action method Name.
            return View("Error", eInfo);
        }
    }
}