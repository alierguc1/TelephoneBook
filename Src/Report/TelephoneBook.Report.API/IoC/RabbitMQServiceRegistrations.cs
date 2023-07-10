using EventBus.Base.Abstraction;
using EventBus.Base;
using EventBus.Factory;
using TelephoneBook.Report.API.MessageBrokerIntegrationEvents.EventHandlers;
using TelephoneBook.Report.API.MessageBrokerIntegrationEvents.Events;
using RabbitMQ.Client;

namespace TelephoneBook.Report.API.IoC
{
    public static class RabbitMQServiceRegistrations
    {
        public static void ConfigureRabbitMQ(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton(sp =>
            {
                EventBusConfig config = new()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = builder.Configuration.GetValue<string>("RabbitMQConfig:EventNameSuffix"),
                    SubscriberClientAppName = builder.Configuration.GetValue<string>("RabbitMQConfig:SubscriberClientAppName"),
                    Connection = new ConnectionFactory()
                    {
                        HostName = builder.Configuration.GetValue<string>("RabbitMQConfig:HostName"),
                        Port = builder.Configuration.GetValue<int>("RabbitMQConfig:Port")
                    },
                    EventBusType = EventBusType.RabbitMQ,

                };

                return EventBusFactory.Create(config, sp);
            });

            builder.Services.AddTransient<ReportCreatingEventHandler>();
        }

        public static void ConfigureEventBusForSubscription(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<ReportCreatingEvent, ReportCreatingEventHandler>();
        }
    }
}
