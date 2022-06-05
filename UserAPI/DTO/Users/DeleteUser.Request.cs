using UserAPI.Domain.Models;

namespace UserAPI.DTO.Users
{
    public class DeleteUserRequest
    {
        public int? UserId { get; set; }
        public string UserMail { get; set; }
    }
}
