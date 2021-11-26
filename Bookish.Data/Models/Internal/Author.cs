using System.Collections.Generic;

namespace Bookish.Data.Models.Internal
{
    public class Author
    {
        public int Id;
        public string Name;
        public List<Book> Books;

        public Author(int id, string name)
        {
            Id = id;
            Name = name;
            Books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            if (Books.Exists(b => b == book)) return;
            Books.Add(book);
        }
        
        public void DisplayToConsole()
        {
            //
        }
        
        public static void DisplayListToConsole(List<Book> books)
        {
            //
        }
    }
}