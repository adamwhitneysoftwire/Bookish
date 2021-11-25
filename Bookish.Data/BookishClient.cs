using System.Collections.Generic;
using System.Data.SqlClient;
using Bookish.Data.Models.Database;
using Bookish.Data.Models.Internal;
using Dapper;

namespace Bookish.Data
{
    
    public interface IBookishClient
    {
        public Book FindBook(int bookId);
        public List<Book> FindAllBooks();
        public List<Book> SearchBooks(string query);
    }
    
    public class BookishClient : IBookishClient
    {
        private readonly SqlConnection _connection;

        public BookishClient()
        {
            _connection = new SqlConnection("Server=localhost;Database=Bookish;Trusted_Connection=True;");
        }
        
        public Book FindBook(int bookId)
        {
            var queryParameters = new { BookId = bookId };
            
            var bookWrappers = _connection.Query<BookWrapper>(@"SELECT Book.Id, Book.Title, Book.Isbn, Author.Id AS AuthorId, Author.Name AS AuthorName
                FROM Book
                LEFT JOIN BookAuthor ON Book.Id = BookAuthor.BookId
                JOIN Author ON BookAuthor.AuthorId = Author.Id
                WHERE Book.Id = @BookId", queryParameters).AsList();

            if (bookWrappers.Count == 0) return null;

            var book = bookWrappers[0].ToBook();
            foreach (var bookWrapper in bookWrappers)
            {
                var author = bookWrapper.ToAuthor();
                book.AddAuthor(author);
            }
            
            FetchBookInstances(book);

            return book;
        }

        public List<Book> FindAllBooks()
        {
            var bookWrappers = _connection.Query<BookWrapper>(@"SELECT Book.Id, Book.Title, Book.Isbn, Author.Id AS AuthorId, Author.Name AS AuthorName
                FROM Book
                LEFT JOIN BookAuthor ON Book.Id = BookAuthor.BookId
                JOIN Author ON BookAuthor.AuthorId = Author.Id
                ORDER BY Book.Title ASC").AsList();

            return ConvertWrappersToBooks(bookWrappers);
        }

        public List<Book> SearchBooks(string query)
        {
            query = $"%{query}%";
            var queryParameters = new {query = query};
            var bookWrappers = _connection.Query<BookWrapper>(@"SELECT Book.Id, Book.Title, Book.Isbn, Author.Id AS AuthorId, Author.Name AS AuthorName
                FROM Book
                LEFT JOIN BookAuthor ON Book.Id = BookAuthor.BookId
                JOIN Author ON BookAuthor.AuthorId = Author.Id
                WHERE Book.Title LIKE @query
                OR Author.Name LIKE @query
                ORDER BY Book.Title ASC", queryParameters).AsList();

            return ConvertWrappersToBooks(bookWrappers);
        }
        
        private List<Book> ConvertWrappersToBooks(List<BookWrapper> bookWrappers)
        {
            var authors = new List<Author>();
            var books = new List<Book>();
            
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

                FetchBookInstances(book);
            }

            return books;
        }

        private void FetchBookInstances(Book book)
        {
            var queryParameters = new {BookId = book.Id};
            var bookInstanceWrappers = _connection.Query<BookInstanceWrapper>(@"SELECT BookInstance.Id, BookCheckout.Id AS CheckoutId, BookCheckout.ReturnDate AS CheckoutReturnDate, BookCheckout.Returned AS CheckoutReturned, LibraryUser.Id AS UserId, LibraryUser.Name AS UserName, LibraryUser.Email AS UserEmail
                    FROM BookInstance
                    LEFT OUTER JOIN BookCheckout ON BookInstance.Id = BookCheckout.InstanceId
                    LEFT OUTER JOIN LibraryUser ON BookCheckout.UserId = LibraryUser.Id
                    WHERE BookId = @BookId", queryParameters).AsList();

            foreach (var bookInstanceWrapper in bookInstanceWrappers)
            {
                var bookInstance = book.Instances.Find(i => i.Id == bookInstanceWrapper.Id);
                if (bookInstance == null)
                {
                    bookInstance = bookInstanceWrapper.ToBookInstance(book);
                    book.AddInstance(bookInstance);
                }

                if (bookInstanceWrapper.CheckoutId > 0)
                {
                    var user = bookInstanceWrapper.ToUser();
                    var checkout = bookInstanceWrapper.ToCheckout(bookInstance, user);
                    bookInstance.AddCheckout(checkout);
                }
            }
        }
    }
}