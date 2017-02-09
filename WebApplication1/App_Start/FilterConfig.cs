using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;     //lab30

namespace WebApplication1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());    //ExceptionFilter     //radilo prvi put, vise ne radi na globalnom, radi kao pojedinacni atribut act. metode [HandleError] (vidi BulkUploadController met. Upload   sve je lab29)
            //filters.Add(new AuthorizeAttribute());    //baca auth. error za svaki URL ??? (lab29, radi ako iskomentirana linija)
            filters.Add(new EmployeeExceptionFilter());     //lab30 za logganje excepcija, globalno, radi, iskomentirana u BulkUploadController/Upload jer radi globalno
        }
    }
}
