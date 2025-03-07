using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Practica_NavegacionEntrePage.Models;

namespace Practica_NavegacionEntrePage.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Users.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        // ✅ Agregar un usuario a la base de datos
        public Task<int> AddUser(User user)
        {
            return _database.InsertAsync(user);
        }

        // ✅ Buscar usuario por email
        public Task<User> GetUserByEmail(string email)
        {
            return _database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        // ✅ Buscar usuario por credenciales (Login)
        public Task<User> GetUserByCredentials(string email, string password)
        {
            return _database.Table<User>().Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
        }
    }
}
