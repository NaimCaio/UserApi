using System;
using UserAPI.Domain.Models;

namespace UserAPI.DTO.Users
{
    public class AddUserRequest
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Escolaridade { get; set; }
    }
}
