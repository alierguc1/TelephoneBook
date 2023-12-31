﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Infrastructure.Concretes
{
    public class ContactsDetailsRepository : IContactsDetailsRepository
    {
        private readonly IMongoCollection<ContactDetail> _contactDetailsCollection;
        private readonly IConfiguration _configuration;
        public ContactsDetailsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var db = client.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _contactDetailsCollection = db.GetCollection<ContactDetail>(_configuration["DatabaseSettings:ContactDetailsCollectionName"]);
        }

        public async Task<ContactDetail> CreateContactDetailsAsync(ContactDetail addContactDetails)
        {
            await _contactDetailsCollection.InsertOneAsync(addContactDetails);
            return addContactDetails;
        }

        public async Task<bool> DeleteContactDetailsAsync(string id)
        {
            var result = await _contactDetailsCollection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<List<ContactDetail>> GetAllContactDetailsListAsync()
        {
            return await _contactDetailsCollection.Find(c => true).ToListAsync();
        }

        public async Task<ContactDetail> GetContactDetailsByContactIdAsync(string contactId)
        {
            return await _contactDetailsCollection.Find(c => true && c.ContactId == contactId).FirstOrDefaultAsync();
        }
    }
}
