using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Report.Business.Concrete;
using TelephoneBook.Report.Business.Interfaces;

namespace TelephoneBook.Report.Business.IoC
{
    public static class BusinessDependencies
    {
        public static IServiceCollection AddBusinessDependencies(this IServiceCollection services)
        {
            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<IReportDetailsRepository, ReportDetailsRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
