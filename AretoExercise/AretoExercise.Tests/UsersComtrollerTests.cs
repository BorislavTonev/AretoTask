using AretoExercise.Application.Interfaces;
using AretoExercise.Controllers;
using AretoExercise.Data.Interfaces;
using AretoExercise.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AretoExercise.Tests
{
    public class UsersComtrollerTests
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
            userServiceMock.Setup(u => u.GetUser(1)).Returns(users[0]);

        }


        private UsersController GenerateUsersController()
        {
            return new UsersController(userServiceMock.Object);
        }

        [Test]
        public void GetUser()
        {
            //arange
            int userIUd = 1;
            var controller = GenerateUsersController();

            //act
            var result = controller.GetUser(userIUd);

            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);

        }
    }
}
