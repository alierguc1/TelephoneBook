using AutoMapper;
using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TelephoneBook.Contact.Shared.Models;
using TelephoneBook.Report.API.MessageBrokerIntegrationEvents.Events;
using TelephoneBook.Report.Business.Interfaces;
using TelephoneBook.Report.Entities.Models;
using TelephoneBook.Report.Entities.ViewModels;

namespace TelephoneBook.Report.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportDetailsRepository _reportDetailsRepository;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;
        public ReportsController(
            IEventBus eventBus,
            IReportRepository reportRepository,
            IMapper mapper,
            IReportDetailsRepository reportDetailsRepository)
        {
            _eventBus = eventBus;
            _mapper = mapper;
            _reportRepository = reportRepository;
            _reportDetailsRepository=reportDetailsRepository;
        }


        [HttpGet("GetAllReports")]
        [ProducesResponseType(typeof(ResponseDatas<IList<ReportIndexViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllReports()
        {
            var list = await _reportRepository.GetAllReportsAsync();

            IList<ReportIndexViewModel> reports = null;
            if (list != null && list.Any())
            {
                reports = _mapper.Map<IList<ReportIndexViewModel>>(list);
            }

            return ResponseDatas<IList<ReportIndexViewModel>>.Success(reports, (int)HttpStatusCode.OK);
        }

        [HttpPost("CreateReport")]
        [ProducesResponseType(typeof(ResponseDatas<ResultId<string>>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateReport()
        {
            var newReport = await _reportRepository.CreateReportAsync();

            if (newReport == null) return BadRequest();

            var reportStartedEventModel = new ReportStartingEvent(newReport.Id);
            _eventBus.Publish(reportStartedEventModel);

            var result = new ResultId<string> { Id = newReport.Id };
            return ResponseDatas<ResultId<string>>.Success(result, (int)HttpStatusCode.Created);
        }


        [HttpGet("GetReportByIdAsync/{id}")]
        [ProducesResponseType(typeof(ResponseDatas<ReportViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetReportById(string id)
        {
            var report = await _reportRepository.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<ReportViewModel>(report);

            var details = await _reportDetailsRepository.GetDetailsByReportIdAsync(report.Id);
            if (details != null && details.Any())
            {
                model.ReportDetails = _mapper.Map<IList<ReportDetailsViewModel>>(details);
            }

            return ResponseDatas<ReportViewModel>.Success(model, (int)HttpStatusCode.OK);
        }
    }
}
