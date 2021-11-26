using System;
using System.Collections.Generic;

namespace Bookish.Data.Models.Internal
{
    public class User
    {
        public string Id;
        public string Username;
        
        public User(string id, string username)
        {
            Id = id;
            Username = username;
        }
        
        public void DisplayToConsole()
        {
            Console.Out.WriteLine($"{Username} (ID: {Id})");
        }
        
        public static void DisplayListToConsole(List<User> users)
        {
            //
        }
    }
}