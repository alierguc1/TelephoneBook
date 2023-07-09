
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Infrastructure.Concretes;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Infrastructure.IoC
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
         
            services.AddScoped<IContactsDetailsRepository, ContactsDetailsRepository>();
            services.AddScoped<IContactsRepository, ContactsRepository>();
         
            return services;
        }
    }
}
