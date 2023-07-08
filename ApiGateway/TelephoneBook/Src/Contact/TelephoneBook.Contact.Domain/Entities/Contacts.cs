using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.Contact.Domain.Entities
{
    public class Contacts
    {
        public Contacts(string @contactName, string @contactLastName, string @contactCompany)
        {
            ContactName = @contactName;
            ContactLastName = @contactLastName;
            ContactCompany = @contactCompany;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ContactName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactCompany { get; set; }
    }
}
