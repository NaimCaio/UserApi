using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Domain.Interfaces
{
    public interface IValidationService
    {
        bool EmailValidation(string email);
        bool DateValidation(DateTime date);
        int EscolaridadeValidation(string escolaridade);
    }
}
