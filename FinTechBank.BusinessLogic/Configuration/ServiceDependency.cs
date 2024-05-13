using FinTechBank.BusinessLogic.Services;
using FinTechBank.DataAccess.Models;
using FinTechBank.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechBank.BusinessLogic.Configuration
{
    public static class ServiceDependency
    {
        public static void ConfigureAppService(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
        }
    }
}
