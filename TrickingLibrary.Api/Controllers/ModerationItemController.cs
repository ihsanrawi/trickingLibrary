using Microsoft.AspNetCore.Mvc;

namespace TrickingLibrary.Api.Controllers
{
    public class ModerationItemController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}