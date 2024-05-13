using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechBank.Domain.RequestModel
{
    public class UsuarioRequest
    {
        public string Correo { get; set; }
        public string Clave { get; set; }
    }
}
