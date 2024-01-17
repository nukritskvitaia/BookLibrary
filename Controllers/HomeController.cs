using BookLibrary.DataAccess;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace BookLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Index action called.");
            var books = _unitOfWork.Books.GetAll();
            return View(books);
        }

        public IActionResult Books()
        {
            _logger.LogInformation("Books action called.");
            var books = _unitOfWork.Books.GetAll();
            return View(books);
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Privacy action called.");
            return View();
        }

        public IActionResult About()
        {
            _logger.LogInformation("About action called.");
            return View();
        }

        public IActionResult BookDetails(string title)
        {
            _logger.LogInformation("BookDetails action called with title: {Title}", title);

            var book = GetBookDetailsByTitle(title);

            if (book == null)
            {
                _logger.LogWarning("Book with title '{Title}' not found.", title);
                return NotFound();
            }

            return View(book);
        }

        private Book GetBookDetailsByTitle(string title)
        {
            _logger.LogInformation("GetBookDetailsByTitle method called with title: {Title}", title);

            var books = _unitOfWork.Books.GetAll();
            var matchingBook = books.FirstOrDefault(book => book.Title == title);

            return matchingBook;
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            var emptyBook = new Book();
            return View(emptyBook);
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Books.Add(book);

                try
                {
                    _unitOfWork.SaveChanges();
                    _logger.LogInformation("Book added successfully. Redirecting to Books action.");
                    return RedirectToAction("Books");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while adding the book.");
                }
            }

            _logger.LogWarning("Model state is invalid. Returning the AddBook view with the book model.");
            return View(book);
        }

        public IActionResult Edit(int id)
        {
            _logger.LogInformation("Edit action called with ID: {Id}", id);

            var book = _unitOfWork.Books.GetById(id);

            if (book == null)
            {
                _logger.LogWarning("Book with ID {Id} not found for editing.", id);
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            _logger.LogInformation("Edit [HttpPost] action called.");

            if (ModelState.IsValid)
            {
                _unitOfWork.Books.Update(book);

                try
                {
                    _unitOfWork.SaveChanges();
                    _logger.LogInformation("Book updated successfully. Redirecting to Books action.");
                    return RedirectToAction("Books");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the book.");
                }
            }

            _logger.LogWarning("Model state is invalid. Returning the Edit view with the book model.");
            return View(book);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Delete action called with ID: {Id}", id);

            var book = _unitOfWork.Books.GetById(id);

            if (book == null)
            {
                _logger.LogWarning("Book with ID {Id} not found for deletion.", id);
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _logger.LogInformation("DeleteConfirmed [HttpPost] action called with ID: {Id}", id);

            _unitOfWork.Books.Delete(id);

            try
            {
                _unitOfWork.SaveChanges();
                _logger.LogInformation("Book deleted successfully. Redirecting to Books action.");
                return RedirectToAction("Books");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book.");
                return View("Delete", id);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("An error occurred. RequestId: {RequestId}", Activity.Current?.Id ?? HttpContext.TraceIdentifier);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}