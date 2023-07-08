using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Infrastructure.Concretes;
using TelephoneBook.Contact.Infrastructure.Configuration;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Infrastructure.IoC
{
    public static class ContactIoC
    {
        public static IServiceCollection AddContactDependencies(this IServiceCollection services)
        {
            services.AddScoped<IContactsDetailsRepository, ContactsDetailsRepository>();
            services.AddScoped<IContactsRepository, ContactsRepository>();
            services.AddSingleton<IMongoDatabaseConfiguration, MongoDatabaseConfiguration>();     
            return services;
        }
    }
}
