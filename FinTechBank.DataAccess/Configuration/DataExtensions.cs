using FinTechBank.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechBank.DataAccess.Configuration
{
    public static class DataExtensions
    {
        public static void ConfigureDataService(this IServiceCollection services)
        {
            services.AddScoped<DbContext, FinTechBankContext>();
            services.AddScoped<FinTechBankContext>();
        }
    }
}
