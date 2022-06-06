using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using UserAPI.Domain.Interfaces;
using UserAPI.Domain.Models;
using UserAPI.Domain.Shared;
using UserAPI.DTO.Users;
using UserAPI.Infra.Repositories;
using UserAPI.Services;
using UserAPI.Services.Interfaces;

namespace UserAPI.Tests
{
    [TestClass]
    public class ApplicationTests :BaseTests
    {
        public IUserService _applicationService;
        public ApplicationTests():base()
        {
        }

        private IRepository<User> _repository;

        [TestInitialize]
        public void Setup()
        {
            
            base.SetupDBTestDependencies();
            var serviceProvider = _services.BuildServiceProvider();
            _applicationService = serviceProvider.GetService<IUserService>();
            _repository = serviceProvider.GetService<IRepository<User>>();
        }
        [TestMethod]
        public void TestAddAndGetUser()
        {

            var userReq = new AddUserRequest()
            {
                Email = "lala@la.com",
                Nome = "teste",
                Sobrenome = "aaa",
                DataNascimento = DateTime.Now.AddYears(-1),
                Escolaridade = "Infantil"
            };
            
            var newUser = _applicationService.AddUser(userReq);
            Assert.AreEqual(newUser.Mensagem, UserConstants.AddUserSuccess);
            var usersResponse = _applicationService.listUsers();
            Assert.AreEqual(usersResponse.Users.Count, 1);

        }
        [TestMethod]
        [DataRow("email errado", -1, "Infantil",1)]
        [DataRow("teste@gmail.com", 1, "Infantil",2)]
        [DataRow("teste@gmail.com", -1, "escolaridadeErrada",3)]
        public void TestAddAFails(string email, int years ,string Escolaridade, int testId)
        {
            var data = DateTime.Now.AddYears(years);
            var userReq = new AddUserRequest()
            {
                Email = email,
                Nome = "teste",
                Sobrenome = "aaa",
                DataNascimento = data,
                Escolaridade = Escolaridade
            };

            var newUser = _applicationService.AddUser(userReq);
            if (testId == 1)
            {
                Assert.AreEqual(newUser.Mensagem, UserConstants.MailErrorMessage);
            }else if (testId == 2)
            {
                Assert.AreEqual(newUser.Mensagem, UserConstants.DateErrorMessage);
            }
            else if (testId == 3)
            {
                Assert.AreEqual(newUser.Mensagem, UserConstants.EscolaridadeErrorMessage);
            }

            var usersResponse = _applicationService.listUsers();
            Assert.AreEqual(usersResponse.Users.Count, 0);

        }
        [TestMethod]
        public void TestDeletes()
        {
            var userReq = new AddUserRequest()
            {
                Email = "lala@la.com",
                Nome = "teste",
                Sobrenome = "teste",
                DataNascimento = DateTime.Now.AddYears(-1),
                Escolaridade = "Infantil"
            };
            var deleteReq = new DeleteUserRequest()
            {
                UserId = 1,
            };

            var newUser = _applicationService.AddUser(userReq);
            var usersResponseBefore = _applicationService.listUsers();

            var deleteResponse = _applicationService.DeleteUser(deleteReq);

            var usersResponseAfter = _applicationService.listUsers();



            Assert.AreEqual(usersResponseBefore.Users.Count, 1);
            Assert.AreEqual(usersResponseAfter.Users.Count, 0);

        }
        
    }
}
