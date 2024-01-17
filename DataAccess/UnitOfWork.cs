using BookLibrary.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace BookLibrary.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public IBookRepository Books { get; }

        public UnitOfWork(BookContext context, IBookRepository bookRepository, ILogger<UnitOfWork> logger)
        {
            _context = context;
            Books = bookRepository;
            _logger = logger;
        }

        public void SaveChanges()
        {
            _logger.LogInformation("Saving changes to the database.");

            try
            {
                _context.SaveChanges();
                _logger.LogInformation("Changes saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving changes to the database.");
                throw;
            }
        }
    }
}