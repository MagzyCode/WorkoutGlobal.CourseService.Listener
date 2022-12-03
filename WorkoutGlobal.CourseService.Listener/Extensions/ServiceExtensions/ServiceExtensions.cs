using MassTransit;
using Refit;
using WorkoutGlobal.CourseService.Listener.Consumers;
using WorkoutGlobal.CourseService.Listener.Contracts;

namespace WorkoutGlobal.CourseService.Listener.Extensions.ServiceExtensions
{
    /// <summary>
    /// Base class for all service extensions.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure MassTransit.
        /// </summary>
        /// <param name="services">Project services.</param>
        /// <param name="configuration">Project configuration.</param>
        /// <param name="bus">Message broker type.</param>
        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration, Enums.Bus bus)
        {
            services.AddMassTransit(options =>
            {
                options.AddConsumer<UpdateUserConsumer>();
                options.AddConsumer<DeleteUserConsumer>();
                options.AddConsumer<UpdateVideoConsumer>();
                options.AddConsumer<DeleteVideoConsumer>();

                switch (bus)
                {
                    case Enums.Bus.RabbitMQ:
                        options.UsingRabbitMq((context, configurator) =>
                        {
                            configurator.Host(configuration["MassTransitSettings:Hosts:RabbitMQHost"]);
                            configurator.ReceiveEndpoint(configuration["MassTransitSettings:Exchanges:UpdateUser"], endpoint =>
                            {
                                endpoint.ConfigureConsumer<UpdateUserConsumer>(context);
                            });
                            configurator.ReceiveEndpoint(configuration["MassTransitSettings:Exchanges:DeleteUser"], endpoint =>
                            {
                                endpoint.ConfigureConsumer<DeleteUserConsumer>(context);
                            });
                            configurator.ReceiveEndpoint(configuration["MassTransitSettings:Exchanges:UpdateVideo"], endpoint =>
                            {
                                endpoint.ConfigureConsumer<UpdateVideoConsumer>(context);
                            });
                            configurator.ReceiveEndpoint(configuration["MassTransitSettings:Exchanges:DeleteVideo"], endpoint =>
                            {
                                endpoint.ConfigureConsumer<DeleteVideoConsumer>(context);
                            });
                        });
                        break;
                    case Enums.Bus.AzureServiceBus:
                        options.UsingAzureServiceBus((context, configurator) =>
                        {
                            configurator.Host(configuration["MassTransitSettings:Hosts:AzureSBHost"]);
                            configurator.ReceiveEndpoint(configuration["MassTransitSettings:Exchanges:UpdateUser"], endpoint =>
                            {
                                endpoint.ConfigureConsumer<UpdateUserConsumer>(context);
                            });
                            configurator.ReceiveEndpoint(configuration["MassTransitSettings:Exchanges:DeleteUser"], endpoint =>
                            {
                                endpoint.ConfigureConsumer<DeleteUserConsumer>(context);
                            });
                            configurator.ReceiveEndpoint(configuration["MassTransitSettings:Exchanges:UpdateVideo"], endpoint =>
                            {
                                endpoint.ConfigureConsumer<UpdateVideoConsumer>(context);
                            });
                            configurator.ReceiveEndpoint(configuration["MassTransitSettings:Exchanges:DeleteVideo"], endpoint =>
                            {
                                endpoint.ConfigureConsumer<DeleteVideoConsumer>(context);
                            });
                        });
                        break;
                }
            });
        }

        public static void ConfigureRefitServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<ICourseService>()
                .ConfigureHttpClient(configure =>
                {
                    configure.BaseAddress = new(configuration["MassTransitSettings:ConsumerUrl"]);
                });
            services.AddRefitClient<ILessonService>()
                .ConfigureHttpClient(configure =>
                {
                    configure.BaseAddress = new(configuration["MassTransitSettings:ConsumerUrl"]);
                });
        }

    }
}
