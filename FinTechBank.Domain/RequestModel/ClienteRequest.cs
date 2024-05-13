﻿using FinTechBank.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechBank.Domain.RequestModel
{
    public class ClienteRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public TipoClienteEnum? TipoCliente { get; set; }
        public string EstadoCivil { get; set; }
        public string NumeroIdentifiacion { get; set; }
        public string Profesion { get; set; }
        public string Genero { get; set; }
        public string Nacionalidad { get; set; }
    }

    public class PutClienteRequest
    {
        public long IdCliente { get; set; }
    }

    public class DeleteClienteRequest
    {
        public long IdCliente { get; set; }
    }
}
