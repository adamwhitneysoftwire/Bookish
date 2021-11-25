using System.Collections.Generic;
using Bookish.Data;
using Bookish.Data.Models.Internal;

namespace Bookish.Web.Services
{
    public interface IBooksService
    {
        public List<Book> FindAllBooks();
        public List<Book> SearchBooks(string query);
    }
    public class BooksService : IBooksService
    {
        private IBookishClient _bookishClient;

        public BooksService(IBookishClient bookishClient)
        {
            _bookishClient = bookishClient;
        }
        
        public List<Book> FindAllBooks()
        {
            return _bookishClient.FindAllBooks();
        }
        
        public List<Book> SearchBooks(string query)
        {
            return _bookishClient.SearchBooks(query);
        }
    }
}