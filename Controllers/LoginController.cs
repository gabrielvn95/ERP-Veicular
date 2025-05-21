using Microsoft.AspNetCore.Mvc;

namespace GestVeicular.Controllers
{
    public class LoginController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
