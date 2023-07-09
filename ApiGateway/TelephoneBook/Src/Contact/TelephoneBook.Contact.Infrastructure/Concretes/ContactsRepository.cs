using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Infrastructure.Concretes
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly IMongoCollection<Contacts> _contactCollection;
        private readonly IConfiguration _configuration;
        public ContactsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var coll = _configuration["DatabaseSettings:ContactCollectionName"];
            var client = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var db = client.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _contactCollection = db.GetCollection<Contacts>(_configuration["DatabaseSettings:ContactCollectionName"]);
        }
        public async Task<Contacts> CreateContactAsync(Contacts addContact)
        {
            await _contactCollection.InsertOneAsync(addContact);

            return addContact;
        }

        public async Task<bool> DeleteContactAsync(string id)
        {
            var result = await _contactCollection.DeleteOneAsync(x => x.Id == id);

            return result.DeletedCount > 0;
        }

        public async Task<List<Contacts>> GetAllContactsListAsync()
        {
            return await _contactCollection.Find(c => true).ToListAsync();
        }

        public async Task<Contacts> GetContactByIdAsync(string contactId)
        {
            return await _contactCollection.Find(c => true && c.Id == contactId).FirstOrDefaultAsync();
        }
    }
}
