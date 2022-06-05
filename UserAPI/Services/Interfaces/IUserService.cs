using UserAPI.Domain.Models;
using UserAPI.DTO.Users;

namespace UserAPI.Services.Interfaces
{
    public interface IUserService
    {
        ListUsersResponse listUsers();
        AddUserResponse AddUser(AddUserRequest userRequest);

        DeleteUserResponse DeleteUser(DeleteUserRequest deleteUserRequest);
        EditUserResponse EditUser(User editedUser);
    }
}
