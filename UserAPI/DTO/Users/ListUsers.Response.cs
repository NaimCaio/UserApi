using System.Collections.Generic;
using UserAPI.Domain.Models;

namespace UserAPI.DTO.Users
{
    public class ListUsersResponse
    {
        public List<User> Users { get; set; }
        public ListUsersResponse(List<User> users)
        {
            Users = users;
        }
    }
}
