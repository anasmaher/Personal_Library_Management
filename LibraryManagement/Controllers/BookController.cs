using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using LibraryManagement.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.Identity;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly IlibraryRepo<Book> bookRepo;
        private readonly IlibraryRepo<Author> authorRepo;
        private readonly IWebHostEnvironment hosting;

        public BookController(IlibraryRepo<Book> bookRepo, IlibraryRepo<Author> authorRepo, IWebHostEnvironment hosting)
        {
            this.bookRepo = bookRepo;
            this.authorRepo = authorRepo;
            this.hosting = hosting;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var books = bookRepo.GetAll();
            return View(books);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var book = bookRepo.GetById(id);
            return View(book);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var bookAuthorVM = new BookAuthorVM
            {
                Authors = authorRepo.GetAll().ToList() 
            };

            return View(bookAuthorVM);
        }

        [HttpPost]
        public ActionResult Create(BookAuthorVM model)
        {
            try
            {
                // If adding a new author while creating a book.
                if(model.AuthorId == 0 && !string.IsNullOrEmpty(Request.Form["NewAuthorName"]))
                {

                    var author = new Author
                    {
                        Name = Request.Form["NewAuthorName"]
                    };
                    author.UserId = User.Identity.GetUserId();
                    author.Image = Request.Form.Files["NewAuthorPhoto"];

                    authorRepo.Add(author);

                    model.AuthorId = author.Id;
                }

                bookRepo.Add(model);
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
            var book = bookRepo.GetById(id);

            var bookAuthorVM = new BookAuthorVM
            {
                Id = book.Id,
                Title = book.Name,
                Description = book.Description,
                AuthorId = book.AuthorId,
                Authors = authorRepo.GetAll().ToList(),
                PhotoUrl = book.PhotoUrl
            };

            return View(bookAuthorVM);
        }

        [HttpPost]
        public ActionResult Edit(BookAuthorVM model)
        {
            try
            {
                bookRepo.Update(model);
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
            var res = bookRepo.GetById(id);
            return View(res);
        }

        [HttpPost]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepo.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            try
            {
                if(query is not null)
                {
                    var res = bookRepo.Search(query);

                    return View("Index", res);
                }
                else
                {
                    var books = bookRepo.GetAll();

                    return View("Index", books);
                }
            }
            catch
            {
                return View("Index");
            }
        }
    }
}
