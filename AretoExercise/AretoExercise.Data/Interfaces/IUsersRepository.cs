using AretoExercise.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AretoExercise.Data.Interfaces
{
    public interface IUsersRepository
    {
        bool AddUser(User data);
        bool DeleteUser(int userId);
        User GetUser(int userId);
        Task<User> Authenticate(string userName, string pass);
    }
}
