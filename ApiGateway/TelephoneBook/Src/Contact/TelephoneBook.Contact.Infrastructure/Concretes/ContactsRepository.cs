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
        private readonly IMongoCollection<Contacts> _contactDetailsCollection;
        public ContactsRepository(IMongoDatabaseConfiguration mongoDatabaseConfiguration)
        {
            var client = new MongoClient(mongoDatabaseConfiguration.ConnectionString);
            var db = client.GetDatabase(mongoDatabaseConfiguration.DatabaseName);
            _contactDetailsCollection = db.GetCollection<Contacts>(mongoDatabaseConfiguration.ContactsCollectionName);
        }
        public Task<Contacts> CreateContactAsync(Contacts addContact)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteContactAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Contacts>> GetAllContactsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Contacts> GetContactByIdAsync(string contactId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetContactsHasDocumentForDummyDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}
