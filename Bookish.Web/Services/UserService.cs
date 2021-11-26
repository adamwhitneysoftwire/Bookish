using System.Collections.Generic;
using Bookish.Data;
using Bookish.Data.Models.Internal;

namespace Bookish.Web.Services
{
    public interface IUserService
    {
        public (User, List<Book>) FindUserById(string userId);
    }
    public class UserService : IUserService
    {
        private IBookishClient _bookishClient;

        public UserService(IBookishClient bookishClient)
        {
            _bookishClient = bookishClient;
        }
        
        public (User, List<Book>) FindUserById(string userId)
        {
            return _bookishClient.FindUserById(userId);
        }
    }
}