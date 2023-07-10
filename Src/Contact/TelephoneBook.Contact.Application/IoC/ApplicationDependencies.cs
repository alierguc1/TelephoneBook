using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Infrastructure.Concretes;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Application.IoC
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
