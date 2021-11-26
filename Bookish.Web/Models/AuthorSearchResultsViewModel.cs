using System.Collections.Generic;
using Bookish.Data.Models.Internal;

namespace Bookish.Web.Models
{
    public class AuthorSearchResultsViewModel
    {
        public List<Author> Authors;
        public string Query;

        public AuthorSearchResultsViewModel(string query, List<Author> authors)
        {
            Authors = authors;
            Query = query;
        }
    }
}