using AutoMapper;
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
    public class ReportRepository : IReportRepository
    {
        private readonly IMongoCollection<Reports> _reportCollection;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public ReportRepository(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            var client = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var db = client.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _reportCollection = db.GetCollection<Reports>(_configuration["DatabaseSettings:ReportCollectionName"]);
        }

        public async Task<Reports> CreateReportAsync()
        {
            Reports report = new Reports
            {
                CreatedDate = DateTime.Now,
                Status = Entities.Enums.ReportStatus.ReportPreparing
            };

            var newReports = _mapper.Map<Reports>(report);
            await _reportCollection.InsertOneAsync(newReports);
            return newReports;
        }

        public async Task<IList<Reports>> GetAllReportsAsync()
        {
            return await _reportCollection.Find(x => true).ToListAsync();
        }

        public async Task<Reports> GetReportByIdAsync(string id)
        {
            return await _reportCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task ReportCompletedAsync(string id)
        {
            var filter = Builders<Reports>.Filter.Eq(s => s.Id, id);
            var update = Builders<Reports>.Update
              .Set(s => s.Status, Entities.Enums.ReportStatus.ReportCompleted)
              .Set(s => s.CompletedDate, DateTime.UtcNow);

            await _reportCollection.UpdateOneAsync(filter, update);
        }
    }
}
