using EventBus.Base.Events;

namespace TelephoneBook.Report.API.MessageBrokerIntegrationEvents.Events
{
    public class ReportCreatingEvent : IntegrationEvent
    {
        public string ReportId { get; set; }
        public IList<ReportDetailDto> Details { get; set; }

        public class ReportDetailDto
        {
            public string Location { get; set; }
            public int ContactCount { get; set; }
            public int PhoneNumberCount { get; set; }
        }
    }
}
