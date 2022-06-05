using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Domain.Shared
{
    public class UserConstants
    {
        public const string MailErrorMessage = "E-mail com formato invalido";
        public const string DateErrorMessage = "Data de Nacimento Invalida";
        public const string EscolaridadeErrorMessage = "Escolaridade Invalida";
        public const string AddUserFail = "Falha ao adicionar Usuário => ";
        public const string AddUserSuccess = "Usuario adicionado";
        public const string DeleteUserSuccess = "Usuario deletado";
        public const string DeleteUserFail = "Falha ao deletar Usuario=> ";
        public const string UserIdNotFound = "Id de usuário nao encontrado na base";
        public const string UserEmailNotFound = "E-mail de usuário nao encontrado na base";
        public const string EditUserSuccess = "Usuario editado";
        public const string EditUserFail = "Falha ao editar Usuario=> ";
    }
}

