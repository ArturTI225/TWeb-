using System.Web.Mvc;
using System.Web.Security;

namespace ArturTI225.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}