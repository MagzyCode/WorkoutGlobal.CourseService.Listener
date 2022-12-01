using MassTransit;
using Refit;
using System.Text.Json;
using WorkoutGlobal.CourseService.Listener.Contracts;
using WorkoutGlobal.Shared.Messages;

namespace WorkoutGlobal.CourseService.Listener.Consumers
{
    public class UpdateUserConsumer : IConsumer<UpdateUserMessage>
    {
        public UpdateUserConsumer(IConfiguration configuration)
        {
            Configuration = configuration;
            CourseEndpoint = RestService.For<ICourseService>(Configuration["ConsumerUrl"]);
        }

        public IConfiguration Configuration { get; }

        public ICourseService CourseEndpoint { get; }

        public async Task Consume(ConsumeContext<UpdateUserMessage> context)
        {
            var message = context.Message;

            var document = new
            {
                op = "replace",
                path = "/CreatorFullName",
                value = $"{message?.FirstName} {message?.LastName} {message?.Patronymic}"
            };

            var patchDocument = $"[{JsonSerializer.Serialize(document)}]";

            await CourseEndpoint.UpdateCreator(message.UpdationId, patchDocument);
        }
    }
}
