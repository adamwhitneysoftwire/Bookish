using System.Collections.Generic;
using Bookish.Data;
using Bookish.Data.Models.Internal;

namespace Bookish.Web.Services
{
    public interface IBookService
    {
        public Book FindBook(int bookId);
    }
    public class BookService : IBookService
    {
        private IBookishClient _bookishClient;

        public BookService(IBookishClient bookishClient)
        {
            _bookishClient = bookishClient;
        }
        
        public Book FindBook(int bookId)
        {
            return _bookishClient.FindBook(bookId);
        }
    }
}