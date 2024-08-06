using Microsoft.EntityFrameworkCore;
using LibraryManagement.ViewModels;
using LibraryManagement.Models;
using LibraryManagement.Data;
using Microsoft.AspNetCore.Identity;
using LibraryManagement.Services;
namespace LibraryManagement.Repos
{
    public class BookRepo : IlibraryRepo<Book>
    {
        private readonly LibraryManagementContext db;
        private readonly IWebHostEnvironment hosting;
        private readonly UserManager<IdentityUserEx> userManager;
        private readonly GetCurrentUserService currentUser;

        public BookRepo(LibraryManagementContext db, IWebHostEnvironment hosting, UserManager<IdentityUserEx> userManager, GetCurrentUserService currentUser)
        {
            this.db = db;
            this.hosting = hosting;
            this.userManager = userManager;
            this.currentUser = currentUser;
        }

        public void Add<T>(T model)
        {
            if(model is BookAuthorVM bk)
            {
                Book book = new Book
                {
                    Id = bk.Id,
                    Name = bk.Title,
                    Description = bk.Description,
                    AuthorId = bk.AuthorId,
                    UserId = currentUser.GetCurrentUserId(),
                };

                if (bk.Image is not null && bk.Image.Length > 0)
                    book.PhotoUrl = UploadImage(bk);

                if (bk.Pdf is not null && bk.Pdf.Length > 0)
                    book.PdfUrl = UploadPdf(bk);

                db.Books.Add(book);
                SaveOps();
            }
        }

        public void Delete(int id)
        {
            Book book = GetById(id);

            File.Delete("wwwroot/" + book.PhotoUrl);
            File.Delete("wwwroot/" + book.PdfUrl);

            db.Books.Remove(book);
            SaveOps();
        }

        public Book GetById(int id)
        {
            var res = db.Books.Include(b => b.Author).Where(x => x.Id == id).FirstOrDefault();

            return res;
        }

        public void Update<T>(T model)
        {
            if(model is BookAuthorVM bookAuthor)
            {
                Book book = GetById(bookAuthor.Id);

                book.Name = bookAuthor.Title;
                book.Description = bookAuthor.Description;
                book.Author = db.Authors.Find(bookAuthor.AuthorId);
                book.PhotoUrl = book.PhotoUrl;
                book.Id = bookAuthor.Id;
                book.PdfUrl = book.PdfUrl;

                if (bookAuthor.Image is not null && bookAuthor.Image.Length > 0)
                    book.PhotoUrl = UploadImage(bookAuthor);

                if (bookAuthor.Pdf is not null && bookAuthor.Pdf.Length > 0)
                    book.PdfUrl = UploadPdf(bookAuthor);

                db.Books.Update(book);
                SaveOps();
            }
            else if(model is Book book)
            {
                db.Books.Update(book);
                SaveOps();
            }
        }

        public List<Book> GetAll()
        {
            var res = db.Books.Where(x => x.UserId == currentUser.GetCurrentUserId()).ToList();

            return res;
        }

        public List<Book> Search(string query)
        {
            var res = db.Books.Include(x => x.Author)
                .Where(x => x.Name.Contains(query) && x.UserId == currentUser.GetCurrentUserId())
                .ToList();

            return res;
        }

        public void SaveOps()
        {
            db.SaveChanges();
        }

        public string UploadImage<T>(T model)
        {
            string fileName = "";
            if (model is BookAuthorVM bookAuthor)
            {
                fileName = Path.GetFileName(bookAuthor.Image.FileName);
                var filePath = Path.Combine(hosting.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    bookAuthor.Image.CopyTo(stream);
                }
            }
            return "uploads/" + fileName;
        }

        public string UploadPdf(BookAuthorVM book)
        {
            var fileName = Path.GetFileName(book.Pdf.FileName);
            var filePath = Path.Combine(hosting.WebRootPath, "uploads", fileName);

            string newFile = "uploads/" + fileName;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                book.Pdf.CopyTo(stream);
            }
            return newFile;
        }
    }
}
