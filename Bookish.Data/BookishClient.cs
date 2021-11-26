using System;
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
        public (Author, List<Book>) FindAuthor(int authorId);
        public List<Author> FindAllAuthors();
        public List<Author> SearchAuthors(string query);
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
                LEFT OUTER JOIN BookAuthor ON Book.Id = BookAuthor.BookId
                LEFT OUTER JOIN Author ON BookAuthor.AuthorId = Author.Id
                WHERE Book.Id = @BookId
                ORDER BY Author.Name ASC", queryParameters).AsList();

            if (bookWrappers.Count == 0) return null;

            var book = bookWrappers[0].ToBook();
            foreach (var bookWrapper in bookWrappers)
            {
                if (bookWrapper.AuthorId == 0) continue;
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
                LEFT OUTER JOIN BookAuthor ON Book.Id = BookAuthor.BookId
                LEFT OUTER JOIN Author ON BookAuthor.AuthorId = Author.Id
                ORDER BY Book.Title ASC, Author.Name ASC").AsList();

            return ConvertWrappersToBooks(bookWrappers);
        }

        public List<Book> SearchBooks(string query)
        {
            query = $"%{query}%";
            var queryParameters = new {query = query};
            var bookWrappers = _connection.Query<BookWrapper>(@"SELECT Book.Id, Book.Title, Book.Isbn, Author.Id AS AuthorId, Author.Name AS AuthorName
                FROM Book
                LEFT OUTER JOIN BookAuthor ON Book.Id = BookAuthor.BookId
                LEFT OUTER JOIN Author ON BookAuthor.AuthorId = Author.Id
                WHERE Book.Title LIKE @query
                OR Author.Name LIKE @query
                ORDER BY Book.Title ASC, Author.Name ASC", queryParameters).AsList();

            return ConvertWrappersToBooks(bookWrappers);
        }
        
        public (Author, List<Book>) FindAuthor(int authorId)
        {
            var queryParameters = new { AuthorId = authorId };
            
            var authorWrappers = _connection.Query<AuthorWrapper>(@"SELECT Author.Id, Author.Name, Book.Id AS BookId, Book.Title AS BookTitle, Book.Isbn AS BookIsbn
                FROM Author
                LEFT OUTER JOIN BookAuthor ON Author.Id = BookAuthor.AuthorId
                LEFT OUTER JOIN Book ON BookAuthor.BookId = Book.Id
                WHERE Author.Id = @AuthorId
                ORDER BY Book.Title ASC", queryParameters).AsList();

            if (authorWrappers.Count == 0) return (null, null);

            var author = authorWrappers[0].ToAuthor();
            var books = new List<Book>();
            foreach (var authorWrapper in authorWrappers)
            {
                if (authorWrapper.BookId == 0) continue;
                var book = FindBook(authorWrapper.BookId);
                books.Add(book);
            }

            return (author, books);
        }

        public List<Author> FindAllAuthors()
        {
            var authorWrappers = _connection.Query<AuthorWrapper>(@"SELECT Author.Id, Author.Name, Book.Id AS BookId, Book.Title AS BookTitle, Book.Isbn AS BookIsbn
                FROM Author
                LEFT OUTER JOIN BookAuthor ON Author.Id = BookAuthor.AuthorId
                LEFT OUTER JOIN Book ON BookAuthor.BookId = Book.Id
                ORDER BY Author.Name ASC, Book.Title ASC").AsList();

            return ConvertWrappersToAuthors(authorWrappers);
        }

        public List<Author> SearchAuthors(string query)
        {
            query = $"%{query}%";
            var queryParameters = new {query = query};
            var authorWrappers = _connection.Query<AuthorWrapper>(@"SELECT Author.Id, Author.Name, Book.Id AS BookId, Book.Title AS BookTitle, Book.Isbn AS BookIsbn
                FROM Author
                LEFT OUTER JOIN BookAuthor ON Author.Id = BookAuthor.AuthorId
                LEFT OUTER JOIN Book ON BookAuthor.BookId = Book.Id
                WHERE Author.Name LIKE @query
                ORDER BY Author.Name ASC, Book.Title ASC", queryParameters).AsList();

            return ConvertWrappersToAuthors(authorWrappers);
        }
        
        public (User, List<Book>) FindUser(string username)
        {
            var queryParameters = new { Username = username.ToUpper() };
            
            var userWrappers = _connection.Query<UserWrapper>(@"SELECT Id, NormalizedUserName AS Username
                FROM AspNetUsers
                WHERE NormalizedUserName = @Username", queryParameters).AsList();

            if (userWrappers.Count == 0) return (null, null);
            var user = userWrappers[0].ToUser();

            var checkoutParameters = new {UserId = user.Id};
            var bookWithUserCheckoutWrappers = _connection.Query<BookWithUserCheckoutWrapper>(@"SELECT Book.Id, Book.Title, Book.Isbn, Author.Id AS AuthorId, Author.Name AS AuthorName, BookInstance.Id AS BookInstanceId, BookCheckout.Id AS CheckoutId, BookCheckout.ReturnDate AS CheckoutReturnDate, BookCheckout.Returned AS CheckoutReturned
                    FROM Book
                    LEFT OUTER JOIN BookAuthor ON Book.Id = BookAuthor.BookId
                    LEFT OUTER JOIN Author ON BookAuthor.AuthorId = Author.Id
                    JOIN BookInstance ON Book.Id = BookInstance.BookId
                    JOIN BookCheckout ON BookInstance.Id = BookCheckout.InstanceId
                    JOIN AspNetUsers ON BookCheckout.UserId = AspNetUsers.Id
                    WHERE AspNetUsers.Id = @UserId
                    ORDER BY BookCheckout.ReturnDate DESC, Book.Title ASC, Author.Name ASC, BookInstance.Id ASC", checkoutParameters).AsList();

            var books = new List<Book>();
            
            foreach (var bookWithUserCheckoutWrapper in bookWithUserCheckoutWrappers)
            {
                var book = books.Find(b => b.Id == bookWithUserCheckoutWrapper.Id);
                if (book == null)
                {
                    book = bookWithUserCheckoutWrapper.ToBook();
                    books.Add(book);
                }
                
                if (bookWithUserCheckoutWrapper.AuthorId != 0)
                {
                    var author = book.Authors.Find(a => a.Id == bookWithUserCheckoutWrapper.AuthorId);
                    if (author == null)
                    {
                        author = bookWithUserCheckoutWrapper.ToAuthor();
                        book.AddAuthor(author);
                    }
                }
                
                var bookInstance = book.Instances.Find(i => i.Id == bookWithUserCheckoutWrapper.BookInstanceId);
                if (bookInstance == null)
                {
                    bookInstance = bookWithUserCheckoutWrapper.ToBookInstance(book);
                    book.AddInstance(bookInstance);
                }

                var checkout = bookInstance.Checkouts.Find(c => c.Id == bookWithUserCheckoutWrapper.CheckoutId);
                if (checkout == null)
                {
                    checkout = bookWithUserCheckoutWrapper.ToCheckout(user, bookInstance);
                    bookInstance.AddCheckout(checkout);
                }
            }

            return (user, books);
        }

        /*public List<User> FindAllUsers()
        {
        }

        public List<User> SearchUsers(string query)
        {
        }*/

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

                if (bookWrapper.AuthorId != 0)
                {
                    var author = authors.Find(a => a.Id == bookWrapper.AuthorId);
                    if (author == null)
                    {
                        author = bookWrapper.ToAuthor();
                        authors.Add(author);
                    }

                    book.AddAuthor(author);
                }

                FetchBookInstances(book);
            }

            return books;
        }

        private void FetchBookInstances(Book book)
        {
            var queryParameters = new {BookId = book.Id};
            var bookInstanceWrappers = _connection.Query<BookInstanceWrapper>(@"SELECT BookInstance.Id, BookCheckout.Id AS CheckoutId, BookCheckout.ReturnDate AS CheckoutReturnDate, BookCheckout.Returned AS CheckoutReturned, AspNetUsers.Id AS UserId, AspNetUsers.NormalizedUserName AS Username
                    FROM BookInstance
                    LEFT OUTER JOIN BookCheckout ON BookInstance.Id = BookCheckout.InstanceId
                    LEFT OUTER JOIN AspNetUsers ON BookCheckout.UserId = AspNetUsers.Id
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

        private List<Author> ConvertWrappersToAuthors(List<AuthorWrapper> authorWrappers)
        {
            var authors = new List<Author>();
            var books = new List<Book>();
            
            foreach (var authorWrapper in authorWrappers)
            {
                var author = authors.Find(a => a.Id == authorWrapper.Id);
                if (author == null)
                {
                    author = authorWrapper.ToAuthor();
                    authors.Add(author);
                }

                if (authorWrapper.BookId != 0)
                {
                    var book = books.Find(b => b.Id == authorWrapper.BookId);
                    if (book == null)
                    {
                        book = authorWrapper.ToBook();
                        books.Add(book);
                    }

                    author.AddBook(book);
                }
            }

            return authors;
        }
    }
}