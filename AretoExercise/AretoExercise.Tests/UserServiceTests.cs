using AretoExercise.Application.Interfaces;
using AretoExercise.Application.Services;
using AretoExercise.Controllers;
using AretoExercise.Data.Interfaces;
using AretoExercise.Domain;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AretoExercise.Tests
{
    public class UserServiceTests
    {
        private Mock<IUserService> userServiceMock;
        private Mock<IUsersRepository> usersRepositoryMock;
        private List<User> users;

        [SetUp]
        public void Initialize()
        {
            userServiceMock = new Mock<IUserService>();
            usersRepositoryMock = new Mock<IUsersRepository>();
            users = new List<User>()
            {
                new User(){ Id =1 , FirstName ="First", LastName = "User", Username = "fUser",  Password = "password1" },
                new User(){ Id =2 , FirstName ="Second", LastName = "User", Username = "sUser",  Password = "password2" }
            };

            usersRepositoryMock.Setup(u => u.GetUser(1)).Returns(users[0]);
        }

        [Test]
        public void ShouldGetUser()
        {
            //arange
            int userIUd = 1;

            //act
            var userService = new UserService(usersRepositoryMock.Object);
            var result = userService.GetUser(userIUd);

            //assert
            Assert.AreEqual(result , users[0]);
            Assert.AreEqual(result.Id, users[0].Id);

        }
    }
}