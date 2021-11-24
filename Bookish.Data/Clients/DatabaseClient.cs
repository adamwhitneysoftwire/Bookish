using System.Collections.Generic;
using System.Data.SqlClient;
using Bookish.Data.Models.Database;
using Bookish.Data.Models.Internal;
using Dapper;

namespace Bookish.Data.Clients
{
    public class DatabaseClient
    {
        private readonly SqlConnection _connection;

        public DatabaseClient()
        {
            _connection = new SqlConnection("Server=localhost;Database=Bookish;Trusted_Connection=True;");
        }

        public List<Book> FindAllBooks()
        {
            var authors = new List<Author>();
            var books = new List<Book>();
            
            var bookWrappers = _connection.Query<BookWrapper>("SELECT Book.Id, Book.Title, Book.Isbn, Author.Id AS AuthorId, Author.Name AS AuthorName FROM Book LEFT JOIN BookAuthor ON Book.Id = BookAuthor.BookId JOIN Author ON BookAuthor.AuthorId = Author.Id").AsList();
            foreach (var bookWrapper in bookWrappers)
            {
                var book = books.Find(b => b.Id == bookWrapper.Id);
                if (book == null)
                {
                    book = bookWrapper.ToBook();
                    books.Add(book);
                }

                var author = authors.Find(a => a.Id == bookWrapper.AuthorId);
                if (author == null)
                {
                    author = bookWrapper.ToAuthor();
                    authors.Add(author);
                }
                
                book.AddAuthor(author);
            }

            return books;
        }
    }
}