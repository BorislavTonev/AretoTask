using AretoExercise.Application.Interfaces;
using AretoExercise.Data.Interfaces;
using AretoExercise.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AretoExercise.Application.Services
{
    public class UserService : IUserService
    {
        private IUsersRepository _repo;
        public UserService(IUsersRepository repo)
        {
            _repo = repo;
        }
        public bool AddUser(User userInfo)
        {
            try
            {
                return _repo.AddUser(userInfo);
            }
            catch (Exception)
            {
                //Logger added here
                return false;
            }
            
        }

        public  Task<User> AuthenticateUser(string userName, string pass)
        {
            try
            {
                return _repo.Authenticate(userName, pass);
            }
            catch (Exception)
            {
                //Logger added here
                return null;
            }
         
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                return _repo.DeleteUser(userId);
            }
            catch (Exception)
            {
                //Logger added here
                return false;
            }        
        }


        public User GetUser(int userId)
        {
            try
            {
                return _repo.GetUser(userId);
            }
            catch (Exception)
            {
                //Logger added here
                return null;
            }
           
        }
    }
}
