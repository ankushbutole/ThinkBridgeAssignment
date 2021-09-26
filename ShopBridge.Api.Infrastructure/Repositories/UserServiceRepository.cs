using ShopBridge.Api.Application.Helpers;
using ShopBridge.Api.Application.Interfaces;
using ShopBridge.Api.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Api.Infrastructure.Repositories
{
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Ankush", LastName = "Butole", Username = "Admin", Password = "Admin123$", RoleType = "Admin"  }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;
            if (user.RoleType == "Admin")
            {
                return user.WithoutPassword();
            }
            else {
                return null;
            }
           
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}
