using System.Collections.Generic;
using Bookish.Data.Models.Internal;

namespace Bookish.Web.Models
{
    public class AuthorViewModel
    {
        public Author Author;
        public List<Book> Books;

        public AuthorViewModel(Author author, List<Book> books)
        {
            Author = author;
            Books = books;
        }
    }
}