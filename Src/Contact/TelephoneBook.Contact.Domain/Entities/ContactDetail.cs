﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Contact.Domain.Enums;

namespace TelephoneBook.Contact.Domain.Entities
{
    public class ContactDetail
    {
   
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContactId { get; set; }
        public ContactDetailsType ContactDetailsType { get; set; }
        public string Value { get; set; }
    }
}
