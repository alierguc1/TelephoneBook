using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Domain.Entities;

namespace TelephoneBook.Contact.Infrastructure.Interfaces
{
    public interface IContactsRepository
    {
        Task<List<Contacts>> GetAllContactsListAsync();
        Task<Contacts> GetContactByIdAsync(string @contactId);
        Task<Contacts> CreateContactAsync(Contacts @addContact);
        Task<bool> DeleteContactAsync(string @id);
        
    }
}
