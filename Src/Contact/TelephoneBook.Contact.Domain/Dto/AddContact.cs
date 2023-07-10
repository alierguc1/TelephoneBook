using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.Contact.Domain.Dto
{
    public class AddContact
    {
        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactCompany { get; set; }
    }
}
