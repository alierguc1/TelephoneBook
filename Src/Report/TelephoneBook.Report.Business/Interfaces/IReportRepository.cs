using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Report.Entities.Models;

namespace TelephoneBook.Report.Business.Interfaces
{
    public interface IReportRepository
    {
        Task<Reports> CreateReportAsync();
        Task<IList<Reports>> GetAllReportsAsync();
        Task<Reports> GetReportByIdAsync(string @id);
        Task ReportCompletedAsync(string @id);
    }
}
