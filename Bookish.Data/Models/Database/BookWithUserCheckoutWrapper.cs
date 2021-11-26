using System;
using Bookish.Data.Models.Internal;

namespace Bookish.Data.Models.Database
{
    public class BookWithUserCheckoutWrapper
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public long Isbn { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int BookInstanceId { get; set; }
        public int CheckoutId { get; set; }
        public DateTime CheckoutReturnDate { get; set; }
        public bool CheckoutReturned { get; set; }

        public Book ToBook()
        {
            return new Book(Id, Title, Isbn);
        }
        
        public Author ToAuthor()
        {
            return new Author(AuthorId, AuthorName);
        }

        public BookInstance ToBookInstance(Book book)
        {
            return new BookInstance(BookInstanceId, book);
        }

        public Checkout ToCheckout(User user, BookInstance bookInstance)
        {
            return new Checkout(CheckoutId, user, bookInstance, CheckoutReturnDate, CheckoutReturned);
        }
    }
}