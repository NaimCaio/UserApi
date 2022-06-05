using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAPI.Domain.Models
{
    public class Escolaridade
    {
        public string[] Escolaridades { get; set; }

        public Escolaridade(string escolaridadesString)
        {
            Escolaridades = escolaridadesString.Split(",");
        }
    }
}
