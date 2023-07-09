using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneBook.Report.Entities.ViewModels
{
    public class ReportIndexViewModel
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int Status { get; set; }
    }
}
