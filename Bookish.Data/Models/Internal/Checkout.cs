using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
            Id = id;
            User = user;
            BookInstance = bookInstance;
            ReturnDate = returnDate;
            Returned = returned;
        }

        public void DisplayToConsole()
        {
            //
        }
        
        public static void DisplayListToConsole(List<Checkout> checkouts)
        {
            foreach (var checkout in checkouts)
            {
                var bookInstance = checkout.BookInstance;
                var book = bookInstance.Book;
                string authorString = "";
                foreach (var bookAuthor in book.Authors)
                {
                    if (authorString.Length > 0) authorString += ", ";
                    authorString += bookAuthor.Name;
                }

                string returnString;
                if (checkout.Returned)
                {
                    returnString = "Returned";
                }
                else
                {
                    returnString = $"> Due {checkout.ReturnDate:D}";
                }
                System.Console.Out.WriteLine($"{returnString} - {book.Title} - {authorString} ({bookInstance.Id})");
            }
        }
    }
}