using LibraryManagement.Models;
using LibraryManagement.Repos;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IlibraryRepo<Book> bookRepo;

        public HomeController(ILogger<HomeController> logger, IlibraryRepo<Book> bookRepo)
        {
            _logger = logger;
            this.bookRepo = bookRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }
    }
}
