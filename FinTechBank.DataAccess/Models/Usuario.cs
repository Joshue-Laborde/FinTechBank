﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FinTechBank.DataAccess.Models
{
    public partial class Usuario
    {
        public long IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool IdEstado { get; set; }
    }
}