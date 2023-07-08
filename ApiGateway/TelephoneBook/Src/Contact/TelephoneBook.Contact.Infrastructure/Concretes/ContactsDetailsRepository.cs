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
    public class ContactsDetailsRepository : IContactsDetailsRepository
    {
        private readonly IMongoCollection<ContactDetails> _contactDetailsCollection;
        public ContactsDetailsRepository(IMongoDatabaseConfiguration mongoDatabaseConfiguration)
        {
            var client = new MongoClient(mongoDatabaseConfiguration.ConnectionString);
            var db = client.GetDatabase(mongoDatabaseConfiguration.DatabaseName);
            _contactDetailsCollection = db.GetCollection<ContactDetails>(mongoDatabaseConfiguration.ContactDetailsCollectionName);
        }

        public async Task<ContactDetails> CreateContactDetailsAsync(ContactDetails addContactDetails)
        {
            await _contactDetailsCollection.InsertOneAsync(addContactDetails);
            return addContactDetails;
        }

        public async Task<bool> DeleteContactDetailsAsync(string id)
        {
            var result = await _contactDetailsCollection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<List<ContactDetails>> GetAllContactDetailsListAsync()
        {
            return await _contactDetailsCollection.Find(c => true).ToListAsync();
        }

        public async Task<List<ContactDetails>> GetContactDetailsByContactIdAsync(string contactId)
        {
            return await _contactDetailsCollection.Find(c => true && c.ContactId == contactId).ToListAsync();
        }
    }
}
