using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Domain.Entities;
using TelephoneBook.Contact.Infrastructure.Interfaces;

namespace TelephoneBook.Contact.Infrastructure.Concretes
{
    public class ContactsDetailsRepository : IContactsDetailsRepository
    {
        public Task<ContactDetails> CreateContactInfoAsync(ContactDetails addContactDetails)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteContactInfoAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContactDetails>> GetAllContactDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ContactDetails>> GetContactDetailsByContactIdAsync(string contactId)
        {
            throw new NotImplementedException();
        }
    }
}
