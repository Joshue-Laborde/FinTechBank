﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinTechBank.Domain.Dtos
{
    public class GenericResponse
    {
        public HttpStatusCode HttpCode { get; set; }
        public string Message { get; set; }
    }
}
