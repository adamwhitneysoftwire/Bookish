using Bookish.Data.Models.Internal;

namespace Bookish.Data.Models.Database
{
    public class UserWrapper
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public User ToUser()
        {
            return new User(Id, Username);
        }
    }
}