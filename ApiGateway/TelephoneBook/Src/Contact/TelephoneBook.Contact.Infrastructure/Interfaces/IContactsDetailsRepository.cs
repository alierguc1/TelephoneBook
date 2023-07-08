using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Domain.Entities;

namespace TelephoneBook.Contact.Infrastructure.Interfaces
{
    public interface IContactsDetailsRepository
    {
        Task<ContactDetails> CreateContactInfoAsync(ContactDetails @addContactDetails);
        Task<bool> DeleteContactInfoAsync(string @id);
        Task<List<ContactDetails>> GetAllContactDetailsAsync();
        Task<List<ContactDetails>> GetContactDetailsByContactIdAsync(string @contactId);
    }
}
