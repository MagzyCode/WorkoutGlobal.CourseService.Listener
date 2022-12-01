using MassTransit;
using Refit;
using WorkoutGlobal.CourseService.Listener.Contracts;
using WorkoutGlobal.Shared.Messages;

namespace WorkoutGlobal.CourseService.Listener.Consumers
{
    public class DeleteUserConsumer : IConsumer<DeleteUserMessage>
    {
        public DeleteUserConsumer(IConfiguration configuration)
        {
            Configuration = configuration;
            CourseEndpoint = RestService.For<ICourseService>(Configuration["ConsumerUrl"]);
        }

        public IConfiguration Configuration { get; }

        public ICourseService CourseEndpoint { get; }

        public async Task Consume(ConsumeContext<DeleteUserMessage> context)
        {
            await CourseEndpoint.DeleteCreatorCourses(context.Message.DeletionId);
        }
    }
}
