using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Domain.Interfaces;
using UserAPI.Domain.Models;
using UserAPI.DTO.Users;
using UserAPI.Services;
using UserAPI.Services.Interfaces;
using static UserAPI.DTO.Users.AddUser;
using static UserAPI.DTO.Users.ListUsers;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("UserPolicy")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpGet]
        [Route("list")]
        public ActionResult<ListUsersResponse> List()
        {
            return _userService.listUsers();

        }
        [HttpPost]
        [Route("add")]
        public ActionResult<AddUserResponse> Add([FromBody] AddUserRequest newUserRequest)
        {
            return _userService.AddUser(newUserRequest);

        }
        [HttpDelete]
        [Route("delete")]
        public ActionResult<DeleteUserResponse> Delete([FromBody] DeleteUserRequest deleteUserRequest)
        {
            return _userService.DeleteUser(deleteUserRequest);

        }
        [HttpPost]
        [Route("edit")]
        public ActionResult<EditUserResponse> Edit([FromBody] User editUser)
        {
            return _userService.EditUser(editUser);

        }
    }
}
