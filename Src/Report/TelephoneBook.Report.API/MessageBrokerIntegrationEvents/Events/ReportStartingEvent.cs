using EventBus.Base.Events;

namespace TelephoneBook.Report.API.MessageBrokerIntegrationEvents.Events
{
    public class ReportStartingEvent : IntegrationEvent
    {
        public ReportStartingEvent() { }

        public ReportStartingEvent(string reportId)
        {
            ReportId = reportId;
        }

        public string ReportId { get; set; }
    }
}
