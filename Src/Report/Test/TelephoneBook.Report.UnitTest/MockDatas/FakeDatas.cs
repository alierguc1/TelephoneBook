using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Report.Entities.Enums;
using TelephoneBook.Report.Entities.Models;

namespace TelephoneBook.Report.UnitTest.MockDatas
{
    public static class FakeDatas
    {
        public static string fakeReportId = "64ac190ae6c8bae74f074983";
        public static Reports GetReportById(string fakeReportId)
        {
            return GetReportsFake().FirstOrDefault(x => x.Id == fakeReportId);
        }
        public static IList<Reports> GetReportsFake()
        {
            return new List<Reports>
              {
                new Reports
                {
                    CompletedDate = DateTime.Now,
                    Status = ReportStatus.ReportCompleted,
                    Id = "64ac190ae6c8bae74f074983"
                },
                new Reports
                {
                    CompletedDate = DateTime.Now,
                    Status = ReportStatus.ReportPreparing,
                    Id = "64ac1912ee6abc0f6d7eead2"
                },
              };
        }
    }
}
