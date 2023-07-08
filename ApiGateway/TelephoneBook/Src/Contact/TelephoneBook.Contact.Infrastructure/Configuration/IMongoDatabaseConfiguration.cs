using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.Contact.Infrastructure.Configuration
{
    public interface IMongoDatabaseConfiguration
    {
        public string ContactsCollectionName { get; set; }
        public string ContactDetailsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
