using AretoExercise.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AretoExercise.Application.Interfaces
{
    public interface IUserService
    {
        User GetUser(int userId);
        bool AddUser(User userInfo);
        bool DeleteUser(int userId);
        Task<User> AuthenticateUser(string userName, string pass);
    }
}
