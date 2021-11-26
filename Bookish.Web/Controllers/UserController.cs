using System.Collections.Generic;
using Bookish.Data.Models.Internal;
using Bookish.Web.Models;
using Bookish.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookish.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        
        // GET
        public IActionResult Index([FromQuery] string id)
        {
            (User user, List<Book> books) = _userService.FindUserById(id);
            return View(new UserViewModel(user, books));
        }
    }
}