using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Report.Entities.Models;

namespace TelephoneBook.Report.Business.Interfaces
{
    public interface IReportDetailsRepository
    {
        Task<IList<ReportDetails>> GetDetailsByReportIdAsync(string @reportId);
        Task CreateReportDetailsAsync(IList<ReportDetails> @addReportDetails);
    }
}
