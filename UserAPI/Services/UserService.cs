using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using UserAPI.Domain.Interfaces;
using UserAPI.Domain.Models;
using UserAPI.Domain.Shared;
using UserAPI.DTO.Users;
using UserAPI.Services.Interfaces;
using static UserAPI.DTO.Users.ListUsers;

namespace UserAPI.Services
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> _usersRepository;
        private readonly IValidationService _validationService;
        private readonly ILogger<UserService> _log;
        public UserService(IRepository<User> usersRepository, IValidationService validationService, ILogger<UserService> log)
        {
            _usersRepository = usersRepository;
            _validationService = validationService;
            _log = log;
        }
        public ListUsersResponse listUsers()
        {
            var users =_usersRepository.List().ToList();
            var usersResponse =new ListUsersResponse(users);
            return usersResponse;
        }

        public AddUserResponse AddUser(AddUserRequest userRequest)
        {
            var mailValidation = _validationService.EmailValidation(userRequest.Email);
            if(mailValidation == false)
            {
                _log.LogError(UserConstants.MailErrorMessage);
                return new AddUserResponse() { Mensagem = UserConstants.MailErrorMessage };
            }
            var dateValidation = _validationService.DateValidation(userRequest.DataNascimento);
            if (dateValidation == false)
            {
                _log.LogError(UserConstants.DateErrorMessage);
                return new AddUserResponse() { Mensagem = UserConstants.DateErrorMessage };
            }
            var escolaridadeValidation = _validationService.EscolaridadeValidation(userRequest.Escolaridade);
            if (escolaridadeValidation < 0)
            {
                _log.LogError(UserConstants.DateErrorMessage);
                return new AddUserResponse() { Mensagem = UserConstants.EscolaridadeErrorMessage };
            }
            var newUser = new User()
            {
                Nome = userRequest.Nome,
                Sobrenome = userRequest.Sobrenome,
                DataNascimento = userRequest.DataNascimento,
                Email = userRequest.Email,
                Escolaridade = escolaridadeValidation,
            };
            try
            {
                _usersRepository.Add(newUser);
                _usersRepository.Save();
                _log.LogInformation(UserConstants.AddUserSuccess);
            }
            catch (Exception ex)
            {
                _log.LogError(UserConstants.AddUserFail);
                throw new Exception(UserConstants.AddUserFail + ex.Message);
            }

            var newUserResponse = new AddUserResponse()
            {
                ReturnedAddUser = newUser,
                Mensagem = UserConstants.AddUserSuccess
            };
            return newUserResponse;
        }

        public DeleteUserResponse DeleteUser(DeleteUserRequest deleteUserRequest)
        {
            var dbUser = new User();
            var deleteResponse = new DeleteUserResponse();
            if (deleteUserRequest.UserId != null)
            {
                //Deltar por ID
                dbUser  = _usersRepository.FirstOrDeafault(u=> u.Id== deleteUserRequest.UserId);
                if (dbUser == null)
                {
                    deleteResponse = new DeleteUserResponse();
                    _log.LogInformation(UserConstants.UserIdNotFound);
                    return deleteResponse;
                }

            }
            else
            {
                //Deltar por email
                dbUser = _usersRepository.FirstOrDeafault(u => u.Email == deleteUserRequest.UserMail);
                if (dbUser == null)
                {
                    deleteResponse = new DeleteUserResponse();
                    _log.LogError(UserConstants.UserEmailNotFound);
                    return deleteResponse;
                }
            }

            try
            {
                _usersRepository.Remove(dbUser);
                _usersRepository.Save();
                _log.LogInformation(UserConstants.DeleteUserSuccess);
            }
            catch (Exception ex)
            {

                _log.LogError(UserConstants.DeleteUserFail);
                throw new Exception(UserConstants.AddUserFail + ex.Message);
            }

            var usersResponse = new DeleteUserResponse() { UserDeleted=dbUser };
            return usersResponse;
        }

        public EditUserResponse EditUser(User editedUser)
        {
            var editResponse = new EditUserResponse() { EditedUser = editedUser };
            try
            {
                _usersRepository.Edit(editedUser);
                _usersRepository.Save();
                _log.LogInformation(UserConstants.EditUserSuccess);
                editResponse.message = UserConstants.EditUserSuccess;
            }
            catch (Exception ex)
            {

                _log.LogError(UserConstants.EditUserFail);
                throw new Exception(UserConstants.EditUserFail + ex.Message);
            }

            return editResponse;
        }
    }
}
