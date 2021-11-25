using System.Collections.Generic;

namespace Bookish.Data.Models.Internal
{
    public class BookInstance
    {
        public int Id;
        public Book Book;
        public List<Checkout> Checkouts;

        public BookInstance(int id, Book book)
        {
            Id = id;
            Book = book;
            Checkouts = new List<Checkout>();
        }

        public void AddCheckout(Checkout checkout)
        {
            Checkouts.Add(checkout);
        }
    }
}