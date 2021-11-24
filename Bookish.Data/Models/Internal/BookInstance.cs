namespace Bookish.Data.Models.Internal
{
    public class BookInstance
    {
        public int Id;
        public Book Book;

        public BookInstance(int id, Book book)
        {
            Id = id;
            Book = book;
        }
    }
}