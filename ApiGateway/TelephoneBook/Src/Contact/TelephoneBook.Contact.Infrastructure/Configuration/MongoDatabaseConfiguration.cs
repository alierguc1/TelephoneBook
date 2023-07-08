using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.Contact.Infrastructure.Configuration
{
    public class MongoDatabaseConfiguration : IMongoDatabaseConfiguration
    {
        public string ContactsCollectionName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ContactDetailsCollectionName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DatabaseName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
