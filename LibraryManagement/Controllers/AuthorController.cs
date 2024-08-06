using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Repos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IlibraryRepo<Author> authorRepo;
        private readonly IWebHostEnvironment hosting;
        private readonly IlibraryRepo<Book> bookRepo;

        public AuthorController(IlibraryRepo<Author> authorRepo, IWebHostEnvironment hosting, IlibraryRepo<Book> bookRepo)
        {
            this.authorRepo = authorRepo;
            this.hosting = hosting;
            this.bookRepo = bookRepo;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var authors = authorRepo.GetAll();
            return View(authors);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var res = authorRepo.GetById(id);
            return View(res);
        }

        [HttpGet]
        public ActionResult ShowBooksOfAuthor(int id)
        {
            var res = bookRepo.GetAll().Where(x => x.AuthorId == id).ToList();

            // Pass the author name to the view, just to print it on the screen.
            ViewData["Author"] = authorRepo.GetById(id).Name;
            ViewBag.AuthorId = id;

            return View(res);
        }

        //TODO
        [HttpGet]
        public ActionResult AddBooksToAuthor(int id)
        {
            // Pass author id just to pass it to the post method.
            ViewBag.AuthorId = id;

            // Show books that arent related to the current author.
            var res = bookRepo.GetAll().Where(x => x.AuthorId != id).ToList();
            return View(res);
        }

        [HttpPost]
        public ActionResult AddBooksToAuthorPost(int id)
        {
            var bookId = int.Parse(Request.Form["BookId"]);

            var book = bookRepo.GetById(bookId);
            var author = authorRepo.GetById(id);

            book.AuthorId = id;
            book.Author = author;

            bookRepo.Update(book);

            var res = bookRepo.GetAll().Where(x => x.AuthorId != id).ToList();
            return View("AddBooksToAuthor", res);
        }

        [HttpPost]
        public ActionResult SearchAuthorBook(string query)
        {
            var res = bookRepo.Search(query);
            return View("AddBooksToAuthor", res);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Author author)
        {
            try
            {
                author.UserId = User.Identity.GetUserId();

                authorRepo.Add(author);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var author = authorRepo.GetById(id);

            return View(author);
        }

        [HttpPost]
        public ActionResult Edit(Author author)
        {
            try
            {
                authorRepo.Update(author);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var res = authorRepo.GetById(id);

            return View(res);
        }

        [HttpPost]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                authorRepo.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete", authorRepo.GetById(id));
            }
        }

        // Searching in the Index page
        public ActionResult Search(string query)
        {
            var res = authorRepo.Search(query);

            return View("Index", res);
        }
    }
}
