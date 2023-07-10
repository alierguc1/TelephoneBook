using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.Report.Entities.Models
{
    public class ReportDetails
    {
        public ReportDetails(string reportId, string location, int contactCount, int phoneNumberCount)
        {
            ReportId = reportId;
            Location = location;
            ContactCount = contactCount;
            PhoneNumberCount = phoneNumberCount;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReportId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Location { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int ContactCount { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int PhoneNumberCount { get; set; }
    }
}
