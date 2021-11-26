using System.Collections.Generic;
using Bookish.Data;
using Bookish.Data.Models.Internal;

namespace Bookish.Web.Services
{
    public interface IAuthorsService
    {
        public List<Author> FindAllAuthors();
        public List<Author> SearchAuthors(string query);
    }
    public class AuthorsService : IAuthorsService
    {
        private IBookishClient _bookishClient;

        public AuthorsService(IBookishClient bookishClient)
        {
            _bookishClient = bookishClient;
        }
        
        public List<Author> FindAllAuthors()
        {
            return _bookishClient.FindAllAuthors();
        }
        
        public List<Author> SearchAuthors(string query)
        {
            return _bookishClient.SearchAuthors(query);
        }
    }
}