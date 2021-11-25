using System;

namespace Bookish.Data.Models.Internal
{
    public class Checkout
    {
        public int Id;
        public User User;
        public BookInstance BookInstance;
        public DateTime ReturnDate;
        public bool Returned;

        public Checkout(int id, User user, BookInstance bookInstance, DateTime returnDate, bool returned)
        {
            User = user;
            BookInstance = bookInstance;
            ReturnDate = returnDate;
            Returned = returned;
        }
    }
}