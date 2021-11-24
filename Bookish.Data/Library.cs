using System.Collections.Generic;
using Bookish.Data.Clients;
using Bookish.Data.Models.Internal;

namespace Bookish.Data
{
    public class Library
    {
        private DatabaseClient _databaseClient;
        
        public Library()
        {
            _databaseClient = new DatabaseClient();
        }

        public List<Book> FindAllBooks()
        {
            return _databaseClient.FindAllBooks();
        }
    }
}