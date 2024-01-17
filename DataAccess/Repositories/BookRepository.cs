using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary.DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(BookContext context, ILogger<BookRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Book> GetAll()
        {
            _logger.LogInformation("Retrieving all books from the database.");
            return _context.Books.ToList();
        }

        public Book GetById(int id)
        {
            _logger.LogInformation("Retrieving book by ID: {BookId}", id);
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public void Add(Book book)
        {
            _logger.LogInformation("Adding a new book to the database: {BookTitle}", book.Title);
            _context.Books.Add(book);

            try
            {
                _context.SaveChanges();
                _logger.LogInformation("Book added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a book to the database.");
                throw;
            }
        }

        public void Update(Book book)
        {
            _logger.LogInformation("Updating book with ID: {BookId}", book.Id);
            _context.Books.Update(book);

            try
            {
                _context.SaveChanges();
                _logger.LogInformation("Book updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a book in the database.");
                throw;
            }
        }

        public void Delete(int id)
        {
            _logger.LogInformation("Deleting book with ID: {BookId}", id);
            var book = _context.Books.Find(id);

            if (book != null)
            {
                _context.Books.Remove(book);

                try
                {
                    _context.SaveChanges();
                    _logger.LogInformation("Book deleted successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting a book from the database.");
                    throw;
                }
            }
            else
            {
                _logger.LogWarning("Book with ID {BookId} not found for deletion.", id);
            }
        }
    }
}