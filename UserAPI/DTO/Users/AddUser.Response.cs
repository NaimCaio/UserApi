using UserAPI.Domain.Models;

namespace UserAPI.DTO.Users
{
    public class AddUserResponse
    {
        public User ReturnedAddUser { get; set; }
        public string Mensagem { get; set; }
    }
}
