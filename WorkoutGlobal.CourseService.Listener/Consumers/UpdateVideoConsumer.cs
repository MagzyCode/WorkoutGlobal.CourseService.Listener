using MassTransit;
using Refit;
using System.Text.Json;
using WorkoutGlobal.CourseService.Listener.Contracts;
using WorkoutGlobal.Shared.Messages;

namespace WorkoutGlobal.CourseService.Listener.Consumers
{
    public class UpdateVideoConsumer : IConsumer<UpdateVideoMessage>
    {
        public UpdateVideoConsumer(IConfiguration configuration)
        {
            Configuration = configuration;
            LessonEndpoint = RestService.For<ILessonService>(Configuration["ConsumerUrl"]);
        }

        public IConfiguration Configuration { get; }

        public ILessonService LessonEndpoint { get; }

        public async Task Consume(ConsumeContext<UpdateVideoMessage> context)
        {
            var message = context.Message;

            var documents = new[]
            {
                new { op = "replace",  path = "/VideoId", value = message.UpdationId},
                new { op = "replace",  path = "/VideoTitle", value = message.Title},
                new { op = "replace",  path = "/VideoDescription", value = message.Description }
            };

            var patchDocument = JsonSerializer.Serialize(documents);

            await LessonEndpoint.UpdateLessonsVideoInfo(message.UpdationId, patchDocument);
        }
    }
}
