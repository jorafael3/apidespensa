using DespensaAPI.Context;
using DespensaAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DespensaAPI.Services
{
    public class UserService : IUserService
    {
        //private readonly List<UserModel> _users = new List<UserModel>
        //{
        //    new UserModel { Username = "jorge", Password = "12345" }
        //};

        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserModel> AuthenticateAsync(string username, string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);

            // Return null if user not found
            if (user == null)
                return null;

            // Authentication successful
            return user;
        }

        //public async Task<UserModel> AuthenticateAsync(string username, string password)
        //{
        //    var user = await Task.Run(() => _users.SingleOrDefault(u => u.Username == username && u.Password == password));

        //    // Return null if user not found
        //    if (user == null)
        //        return null;

        //    // Authentication successful
        //    return user;
        //}

        // Implementa otros métodos de IUserService si los necesitas
    }
}
