using System.Collections.Generic;
using System.Linq;
using Bookish.Data.Models.Internal;

namespace Bookish.Web.Models
{
    public class UserViewModel
    {
        public User User;
        public List<Checkout> CurrentCheckouts;
        public List<Checkout> PreviousCheckouts;

        public UserViewModel(User user, List<Book> books)
        {
            User = user;
            
            var currentCheckouts = new List<Checkout>();
            var currentCheckoutBooks = books.FindAll(b => b.Instances.Exists(i => i.Checkouts.Exists(c => c.Returned == false)));
            foreach (var bookInstance in currentCheckoutBooks.SelectMany(b => b.Instances))
            {
                currentCheckouts.AddRange(bookInstance.Checkouts.FindAll(c => c.Returned == false));
            }

            CurrentCheckouts = currentCheckouts;
            
            var previousCheckouts = new List<Checkout>();
            var previousCheckoutBooks = books.FindAll(b => b.Instances.Exists(i => i.Checkouts.Exists(c => c.Returned)));
            foreach (var bookInstance in previousCheckoutBooks.SelectMany(b => b.Instances))
            {
                previousCheckouts.AddRange(bookInstance.Checkouts.FindAll(c => c.Returned == true));
            }
            
            PreviousCheckouts = previousCheckouts;
        }
    }
}