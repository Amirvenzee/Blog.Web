using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    
    public class HomeController : AdminControllerBase
    {
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
