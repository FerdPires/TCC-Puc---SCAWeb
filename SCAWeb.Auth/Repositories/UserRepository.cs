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
          //  users.Add(new User { Id = 1, Username = "uz00011", Password = "123", Role = "tecnico" });
            users.Add(new User { Id = 2, Username = "uz00012", Password = "123", Role = "admin" });
            users.Add(new User { Id = 1, Username = "uz00013", Password = "123", Role = "user" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
}
