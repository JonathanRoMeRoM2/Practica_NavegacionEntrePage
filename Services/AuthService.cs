using System.Threading.Tasks;
using Practica_NavegacionEntrePage.Models;
using SQLite;

namespace Practica_NavegacionEntrePage.Services
{
    public class AuthService
    {
        private readonly DatabaseService _databaseService = new DatabaseService();

        public async Task<bool> RegisterUser(string name, string email, string password)
        {
            var existingUser = await _databaseService.GetUserByEmail(email);
            if (existingUser != null)
                return false;

            var newUser = new User { Name = name, Email = email, Password = password };
            await _databaseService.AddUser(newUser);
            return true;
        }

        public async Task<User> LoginUser(string email, string password)
        {
            return await _databaseService.GetUserByCredentials(email, password);
        }
    }
}
