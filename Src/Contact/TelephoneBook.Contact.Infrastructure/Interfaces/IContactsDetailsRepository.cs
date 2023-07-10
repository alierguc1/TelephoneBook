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
        Task<ContactDetail> CreateContactDetailsAsync(ContactDetail @addContactDetails);
        Task<bool> DeleteContactDetailsAsync(string @id);
        Task<List<ContactDetail>> GetAllContactDetailsListAsync();
        Task<ContactDetail> GetContactDetailsByContactIdAsync(string @contactId);
    }
}
