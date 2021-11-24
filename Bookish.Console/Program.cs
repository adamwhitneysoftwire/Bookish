using System;
using Bookish.Data;
using Bookish.Data.Models.Internal;

namespace Bookish.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var library = new Library();

            var books = library.FindAllBooks();

            books.Sort((book1, book2) => String.CompareOrdinal(book1.Title, book2.Title));
            
            foreach (var book in books)
            {
                string authorString = "";
                foreach (var bookAuthor in book.Authors)
                {
                    if (authorString.Length > 0) authorString += ", ";
                    authorString += bookAuthor.Name;
                }
                System.Console.Out.WriteLine($"{book.Title} - {authorString}");           
            }
        }
    }
}
