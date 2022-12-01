using MassTransit;
using Refit;
using System.Text.Json;
using WorkoutGlobal.CourseService.Listener.Contracts;
using WorkoutGlobal.Shared.Messages;

namespace WorkoutGlobal.CourseService.Listener.Consumers
{
    public class DeleteVideoConsumer : IConsumer<DeleteVideoMessage>
    {
        public DeleteVideoConsumer(IConfiguration configuration)
        {
            Configuration = configuration;
            LessonEndpoint = RestService.For<ILessonService>(Configuration["ConsumerUrl"]);
        }

        public IConfiguration Configuration { get; }

        public ILessonService LessonEndpoint { get; }

        public async Task Consume(ConsumeContext<DeleteVideoMessage> context)
        {
            var message = context.Message;

            var documents = new[]
            {
                new { op = "remove",  path = "/VideoId" },
                new { op = "remove",  path = "/VideoTitle" },
                new { op = "remove",  path = "/VideoDescription" }
            };

            var patchDocument = JsonSerializer.Serialize(documents);

            await LessonEndpoint.UpdateLessonsVideoInfo(message.DeletedId, patchDocument);
        }
    }
}
