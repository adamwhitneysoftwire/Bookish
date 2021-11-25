using System.Collections.Generic;
using Bookish.Data.Models.Internal;

namespace Bookish.Web.Models
{
    public class BookSearchResultsViewModel
    {
        public List<Book> Books;
        public string Query;

        public BookSearchResultsViewModel(string query, List<Book> books)
        {
            Books = books;
            Query = query;
        }
    }
}