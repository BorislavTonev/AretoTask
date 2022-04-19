using AretoExercise.Data.Interfaces;
using AretoExercise.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AretoExercise.Data.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AretoDBContext _context;

        public UsersRepository(AretoDBContext context)
        {
            _context = context;
        }
        public  bool AddUser(User data)
        {
            _context.Users.Add(data);

            return _context.SaveChanges() > 0;
        }

        public bool DeleteUser(int userId)
        {
            User userToDelete = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

            _context.Remove(userToDelete);

            return _context.SaveChanges() > 0;
        }

        public User GetUser(int userId)
        {
           return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public Task<User> Authenticate(string userName, string pass)
        {
            Task<User> userToAuthenticate = _context.Users.Where(u => u.Username == userName && u.Password == pass).FirstOrDefaultAsync();

            if (userToAuthenticate != null)
            {
                return userToAuthenticate;
            }
            else
            {
                return null;
            }
        }
    }
}
