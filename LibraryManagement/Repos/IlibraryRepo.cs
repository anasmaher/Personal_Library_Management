using LibraryManagement.Models;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Repos
{
    public interface IlibraryRepo<TEntity> where TEntity : class
    {
        void Add<T>(T model);

        TEntity GetById(int id);

        List<TEntity> GetAll();

        void Update<T>(T model);

        void Delete(int id);

        List<TEntity> Search(string val);

        string UploadImage<T>(T entity);

        void SaveOps();
    }
}
