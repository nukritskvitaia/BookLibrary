using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
namespace BookLibrary.DataAccess
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {

        }
    }
}
