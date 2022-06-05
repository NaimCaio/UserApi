using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UserAPI.Domain.Interfaces;
using UserAPI.Domain.Models;

namespace UserAPI.Domain.BaseServices
{
    public class ValidationService : IValidationService
    {
        private readonly Escolaridade _listaEscolaridade;
        public ValidationService(Escolaridade listaEscolaridade)
        {
            _listaEscolaridade=listaEscolaridade;
        }
        public bool EmailValidation(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool DateValidation(DateTime date)
        {
            if(date.Date > DateTime.Now.Date)
            {
                return false;
            }
            return true;
        }
        public int EscolaridadeValidation(string escolaridade)
        {
            var valid = _listaEscolaridade.Escolaridades.ToList().IndexOf(escolaridade);
            return valid;
        }
    }
}
