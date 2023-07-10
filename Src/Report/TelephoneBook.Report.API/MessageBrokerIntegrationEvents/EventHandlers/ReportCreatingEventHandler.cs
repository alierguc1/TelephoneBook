using EventBus.Base.Abstraction;
using TelephoneBook.Report.API.MessageBrokerIntegrationEvents.Events;
using TelephoneBook.Report.Business.Concrete;
using TelephoneBook.Report.Business.Interfaces;
using TelephoneBook.Report.Entities.Models;

namespace TelephoneBook.Report.API.MessageBrokerIntegrationEvents.EventHandlers
{
    public class ReportCreatingEventHandler : IIntegrationEventHandler<ReportCreatingEvent>
    {
        private readonly IReportDetailsRepository _reportDetailsRepository;
        private readonly IReportRepository _reportRepository;
        private readonly ILogger<ReportCreatingEvent> logger;

        public ReportCreatingEventHandler(
            IReportDetailsRepository reportDetailsRepository,
            IReportRepository reportRepository,
            ILogger<ReportCreatingEvent> logger)
        {
            _reportRepository = reportRepository;
            _reportDetailsRepository = reportDetailsRepository;
            this.logger = logger;
        }

        public async Task Handle(ReportCreatingEvent @event)
        {
            logger.LogInformation("@ RabbitMQ Broker Handling: {IntegrationEventId} at TelephoneBook.Report.API - ({@IntegrationEvent})", @event.ReportId, @event);

            var report = await _reportRepository.GetReportByIdAsync(@event.ReportId);
            if (report == null) return;
            try
            {
                await _reportRepository.ReportCompletedAsync(report.Id);
                logger.LogInformation("@ Report Completed : | {IntegrationEventId} | Report Id : " + report.Id);
            }
            catch(Exception ex)
            {
                logger.LogInformation("@ Report Not Completed : | {IntegrationEventId} | Error is : "+ex.Message.ToString());
            }
            if (@event == null) return;
            var details = @event.Details
              .Select(x => new ReportDetails(@event.ReportId, x.Location, x.ContactCount, x.PhoneNumberCount))
              .ToList();

            await _reportDetailsRepository.CreateReportDetailsAsync(details);
        }
    }
}

