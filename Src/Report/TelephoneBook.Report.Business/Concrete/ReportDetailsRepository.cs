using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Report.Business.Interfaces;
using TelephoneBook.Report.Entities.Models;

namespace TelephoneBook.Report.Business.Concrete
{
    public class ReportDetailsRepository : IReportDetailsRepository
    {
        private readonly IMongoCollection<ReportDetails> _reportDetailsCollection;
        private readonly IConfiguration _configuration;
        public ReportDetailsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var db = client.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _reportDetailsCollection = db.GetCollection<ReportDetails>(_configuration["DatabaseSettings:ReportDetailsCollectionName"]);
        }
        public async Task CreateReportDetailsAsync(IList<ReportDetails> addReportDetails)
        {
            await _reportDetailsCollection.InsertManyAsync(addReportDetails);
        }

        public async Task<IList<ReportDetails>> GetDetailsByReportIdAsync(string reportId)
        {
            return await _reportDetailsCollection.Find(x => x.ReportId == reportId).ToListAsync();
        }
    }
}
