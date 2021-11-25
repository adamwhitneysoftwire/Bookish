using Bookish.Web.Models;
using Bookish.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookish.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private IBooksService _booksService;

        public BooksController(ILogger<BooksController> logger, IBooksService booksService)
        {
            _logger = logger;
            _booksService = booksService;
        }
        
        // GET
        public IActionResult Index()
        {
            return View(_booksService.FindAllBooks());
        }
        
        public IActionResult Search([FromQuery] string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return Redirect("/books");
            }
            return View(new BookSearchResultsViewModel(q, _booksService.SearchBooks(q)));
        }
    }
}