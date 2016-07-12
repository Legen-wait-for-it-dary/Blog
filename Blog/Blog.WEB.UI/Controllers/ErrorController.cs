using System.Web.Mvc;

namespace Blog.WEB.UI.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult PageNotFound()
        {
            return View("PageNotFound");
        }

        public ActionResult InternalServerError()
        {
            return View("InternalServerError");
        }

    }
}
