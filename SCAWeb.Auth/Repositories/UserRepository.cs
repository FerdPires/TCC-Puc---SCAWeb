using SCAWeb.Auth.Models;
using System.Collections.Generic;
using System.Linq;

namespace SCAWeb.Auth.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "User1", Password = "123", Role = "admin" });
            users.Add(new User { Id = 2, Username = "User2", Password = "123", Role = "user" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
}
