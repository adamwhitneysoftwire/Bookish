using Bookish.Web.Models;
using Bookish.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookish.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }
        
        // GET
        public IActionResult Index([FromQuery] int id)
        {
            return View(_bookService.FindBook(id));
        }
    }
}