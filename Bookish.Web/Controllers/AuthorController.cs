using System.Collections.Generic;
using Bookish.Data.Models.Internal;
using Bookish.Web.Models;
using Bookish.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookish.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;
        private IAuthorService _authorService;

        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }
        
        // GET
        public IActionResult Index([FromQuery] int id)
        {
            (Author author, List<Book> books) = _authorService.FindAuthor(id);
            return View(new AuthorViewModel(author, books));
        }
    }
}