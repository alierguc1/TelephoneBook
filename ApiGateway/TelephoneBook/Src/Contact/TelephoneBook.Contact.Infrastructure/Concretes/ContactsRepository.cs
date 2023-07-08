using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Infrastructure.Configuration;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Infrastructure.Concretes
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly IMongoCollection<Contacts> _contactCollection;
        public ContactsRepository(IMongoDatabaseConfiguration mongoDatabaseConfiguration)
        {
            var client = new MongoClient(mongoDatabaseConfiguration.ConnectionString);
            var db = client.GetDatabase(mongoDatabaseConfiguration.DatabaseName);
            _contactCollection = db.GetCollection<Contacts>(mongoDatabaseConfiguration.ContactsCollectionName);
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
