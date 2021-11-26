using System.Collections.Generic;

namespace Bookish.Data.Models.Internal
{
    public class Book
    {
        public int Id;
        public string Title;
        public long Isbn;
        public List<Author> Authors;
        public List<BookInstance> Instances;

        public Book(int id, string title, long isbn)
        {
            Id = id;
            Title = title;
            Isbn = isbn;
            Authors = new List<Author>();
            Instances = new List<BookInstance>();
        }

        public void AddAuthor(Author author)
        {
            if (Authors.Exists(a => a == author)) return;
            Authors.Add(author);
        }

        public void AddInstance(BookInstance instance)
        {
            if (Instances.Exists(i => i == instance)) return;
            Instances.Add(instance);
        }
        
        public void DisplayToConsole()
        {
            string authorString = "";
            foreach (var bookAuthor in Authors)
            {
                if (authorString.Length > 0) authorString += ", ";
                authorString += bookAuthor.Name;
            }
            System.Console.Out.WriteLine($"{Title} - {authorString} (ID: {Id}, ISBN: {Isbn})");
            System.Console.Out.WriteLine(Instances.Count > 0 ? "Library Copies:" : "No copies at library.");
            foreach (BookInstance bookInstance in Instances)
            {
                var checkout = bookInstance.Checkouts.FindLast(i => i.Returned == false);
                System.Console.Out.WriteLine(
                    $" - ({bookInstance.Id}) {(checkout != null ? "Checked Out" : "Available")}");
                if (checkout != null)
                {
                    System.Console.Out.WriteLine($" - - Taken by {checkout.User.Name}; Return due {checkout.ReturnDate:D}");
                }
            }
        }
        
        public static void DisplayListToConsole(List<Book> books)
        {
            foreach (var book in books)
            {
                string authorString = "";
                foreach (var bookAuthor in book.Authors)
                {
                    if (authorString.Length > 0) authorString += ", ";
                    authorString += bookAuthor.Name;
                }
                System.Console.Out.WriteLine($"{book.Title} - {authorString} (ID: {book.Id}, ISBN: {book.Isbn})");           
            }
        }
    }
}