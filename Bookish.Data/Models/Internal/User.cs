using System.Collections.Generic;

namespace Bookish.Data.Models.Internal
{
    public class User
    {
        public string Id;
        public string Name;
        public string Email;
        public List<Checkout> Checkouts;
        
        public User(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            Checkouts = new List<Checkout>();
        }
    }
}