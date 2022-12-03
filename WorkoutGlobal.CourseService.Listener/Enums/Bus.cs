using System.ComponentModel;

namespace WorkoutGlobal.CourseService.Listener.Enums
{
    /// <summary>
    /// Represents using message brokers.
    /// </summary>
    public enum Bus
    {
        /// <summary>
        /// RabbitMQ message broker.
        /// </summary>
        [Description("Male sex.")]
        RabbitMQ = 1,
        /// <summary>
        /// Azure Service Bus message broker.
        /// </summary>
        [Description("Male sex.")]
        AzureServiceBus
    }
}
