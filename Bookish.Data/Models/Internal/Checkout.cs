namespace Bookish.Data.Models.Internal
{
    public class Checkout
    {
        public User User;
        public BookInstance BookInstance;

        public Checkout(User user, BookInstance bookInstance)
        {
            User = user;
            BookInstance = bookInstance;
        }
    }
}