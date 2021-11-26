using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Web.Data
{
    public class BookishWebContext : IdentityDbContext
    {
        public BookishWebContext(DbContextOptions<BookishWebContext> options)
            : base(options)
        {
        }
    }
}