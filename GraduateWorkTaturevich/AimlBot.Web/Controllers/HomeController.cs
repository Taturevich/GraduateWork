using System.Web.Mvc;

namespace AimlBotWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "О приложении";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Контакты";

            return View();
        }
    }
}