using Bookish.Web.Models;
using Bookish.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookish.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ILogger<AuthorsController> _logger;
        private IAuthorsService _authorsService;

        public AuthorsController(ILogger<AuthorsController> logger, IAuthorsService authorsService)
        {
            _logger = logger;
            _authorsService = authorsService;
        }
        
        // GET
        public IActionResult Index()
        {
            return View(_authorsService.FindAllAuthors());
        }
        
        public IActionResult Search([FromQuery] string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return Redirect("/authors");
            }
            return View(new AuthorSearchResultsViewModel(q, _authorsService.SearchAuthors(q)));
        }
    }
}