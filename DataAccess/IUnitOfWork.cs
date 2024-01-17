using BookLibrary.DataAccess.Repositories;

namespace BookLibrary.DataAccess
{
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }
        void SaveChanges();
    }
}
