using System.Collections.Generic;
using Bookish.Data;
using Bookish.Data.Models.Internal;

namespace Bookish.Web.Services
{
    public interface IAuthorService
    {
        public (Author, List<Book>) FindAuthor(int authorId);
    }
    public class AuthorService : IAuthorService
    {
        private IBookishClient _bookishClient;

        public AuthorService(IBookishClient bookishClient)
        {
            _bookishClient = bookishClient;
        }
        
        public (Author, List<Book>) FindAuthor(int authorId)
        {
            return _bookishClient.FindAuthor(authorId);
        }
    }
}