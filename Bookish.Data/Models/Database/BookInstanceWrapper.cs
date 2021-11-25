using System;
using Bookish.Data.Models.Internal;

namespace Bookish.Data.Models.Database
{
    public class BookInstanceWrapper
    {
        public int Id { get; set; }
        public int CheckoutId { get; set; }
        public DateTime CheckoutReturnDate { get; set; }
        public int CheckoutReturned { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public BookInstance ToBookInstance(Book book)
        {
            return new BookInstance(Id, book);
        }
        
        public Checkout ToCheckout(BookInstance bookInstance, User user)
        {
            return new Checkout(CheckoutId, user, bookInstance, CheckoutReturnDate, CheckoutReturned == 1);
        }
        
        public User ToUser()
        {
            return new User(UserId, UserName, UserEmail);
        }
    }
}