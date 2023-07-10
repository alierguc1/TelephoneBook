using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.Report.Entities.Models;
using TelephoneBook.Report.Entities.ViewModels;

namespace TelephoneBook.Report.Business.Mapping
{
    public class ReportMapping : Profile
    {
        public ReportMapping()
        {
            CreateMap<Reports, ReportViewModel>().ReverseMap();
            CreateMap<Reports, ReportIndexViewModel>().ReverseMap();
            CreateMap<ReportDetails, ReportDetailsViewModel>().ReverseMap();
        }
    }
}
