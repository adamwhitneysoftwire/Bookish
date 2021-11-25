using System;
using Bookish.Data;
using Bookish.Data.Models.Internal;

namespace Bookish.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookishClient = new BookishClient();

            /*var books = bookishClient.FindAllBooks();

            foreach (var book in books)
            {
                string authorString = "";
                foreach (var bookAuthor in book.Authors)
                {
                    if (authorString.Length > 0) authorString += ", ";
                    authorString += bookAuthor.Name;
                }
                System.Console.Out.WriteLine($"{book.Title} - {authorString}");           
            }*/

            var book = bookishClient.FindBook(1);
            System.Console.Out.WriteLine("done");
        }
    }
}
