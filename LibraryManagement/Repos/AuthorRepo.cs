using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Services;

namespace LibraryManagement.Repos
{
    public class AuthorRepo : IlibraryRepo<Author>
    {
        private readonly LibraryManagementContext db;
        private readonly IWebHostEnvironment hosting;
        private readonly GetCurrentUserService currentUser;

        public AuthorRepo(LibraryManagementContext db, IWebHostEnvironment hosting, GetCurrentUserService currentUser)
        {
            this.db = db;
            this.hosting = hosting;
            this.currentUser = currentUser;
        }

        public void Add<T>(T model)
        {
            if (model is Author author)
            {
                author.PhotoUrl = UploadImage(author);
                
                db.Authors.Add(author);
                SaveOps();
            }
        }

        public void Delete(int id)
        {
            Author author = GetById(id);

            File.Delete("wwwroot/" + author.PhotoUrl);
            db.Authors.Remove(author);

            SaveOps();
        }

        public List<Author> GetAll()
        {
            var res = db.Authors.Include(x => x.Books).
                Where(x => x.UserId == currentUser.GetCurrentUserId())
                .ToList();

            return res;
        }

        public Author GetById(int id)
        {
            var res = db.Authors.Include(x => x.Books).Where(x => x.Id == id).FirstOrDefault();
            return res;
        }

        public void Update<T>(T model)
        {
            if(model is Author author)
            {
                Author currentAuthor = GetById(author.Id);

                currentAuthor.PhotoUrl = currentAuthor.PhotoUrl;
                currentAuthor.Image = author.Image;
                currentAuthor.Name = author.Name;
                currentAuthor.Id = author.Id;

                if (author.Image is not null && author.Image.Length > 0)
                {
                    currentAuthor.PhotoUrl = UploadImage(author);
                }

                db.Authors.Update(currentAuthor);
                SaveOps();
            }
        }

        public List<Author> Search(string query)
        {
            return db.Authors.Where(x => x.Name.Contains(query) && x.UserId == currentUser.GetCurrentUserId()).ToList();
        }

        public void SaveOps()
        {
            db.SaveChanges();
        }

        public string UploadImage<T>(T model)
        {
            string fileName = "";
            if(model is Author author)
            {
                fileName = Path.GetFileName(author.Image.FileName);
                var filePath = Path.Combine(hosting.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    author.Image.CopyTo(stream);
                }
            }
            return "uploads/" + fileName;
        }
    }
}
