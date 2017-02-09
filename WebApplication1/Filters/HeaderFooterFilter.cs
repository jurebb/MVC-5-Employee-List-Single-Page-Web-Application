using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.ViewModels;

namespace WebApplication1.Filters
{
    //makes sure that correct header and footer data is passed to the ViewModel
    public class HeaderFooterFilter : ActionFilterAttribute  //represents the base class for filter attributes //Upgrade simple HeaderFooterFilter class to ActionFilter by inheriting it from ActionFilterAttribute class
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext) //OnActionExecuted will be used to add post processing logic to action method execution.
        {
            ViewResult v = filterContext.Result as ViewResult;
            if(v!=null)     // v will null when v is not a ViewResult
            {
                BaseViewModel bvm = v.Model as BaseViewModel;
                if(bvm!=null)       //bvm will be null when we want a view without Header and footer
                {
                    bvm.UserName = HttpContext.Current.User.Identity.Name;
                    bvm.FooterData = new FooterViewModel();         //SUPER INTERESTING..
                    bvm.FooterData.CompanyName = "Jure Baban | StepByStepSchools"; //Can be set to dynamic value
                    bvm.FooterData.Year = DateTime.Now.Year.ToString();
                }
            }
        }
    }
}