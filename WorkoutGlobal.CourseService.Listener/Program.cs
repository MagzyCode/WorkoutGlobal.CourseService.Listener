using MassTransit;
using Refit;
using WorkoutGlobal.CourseService.Listener.Consumers;
using WorkoutGlobal.CourseService.Listener.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<UpdateUserConsumer>();
    options.AddConsumer<DeleteUserConsumer>();
    options.AddConsumer<UpdateVideoConsumer>();
    options.AddConsumer<DeleteVideoConsumer>();

    options.UsingRabbitMq((cxt, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMqHost"]);

        cfg.ReceiveEndpoint(builder.Configuration["Exchanges:UpdateUser"], endpoint =>
        {
            endpoint.ConfigureConsumer<UpdateUserConsumer>(cxt);
        });

        cfg.ReceiveEndpoint(builder.Configuration["Exchanges:DeleteUser"], endpoint =>
        {
            endpoint.ConfigureConsumer<DeleteUserConsumer>(cxt);
        });

        cfg.ReceiveEndpoint(builder.Configuration["Exchanges:UpdateVideo"], endpoint =>
        {
            endpoint.ConfigureConsumer<UpdateVideoConsumer>(cxt);
        });

        cfg.ReceiveEndpoint(builder.Configuration["Exchanges:DeleteVideo"], endpoint =>
        {
            endpoint.ConfigureConsumer<DeleteVideoConsumer>(cxt);
        });
    });
});
builder.Services.AddMassTransitHostedService();

builder.Services.AddRefitClient<ICourseService>()
    .ConfigureHttpClient(configure =>
    {
        configure.BaseAddress = new(builder.Configuration["ConsumerUrl"]);
    });

builder.Services.AddRefitClient<ILessonService>()
    .ConfigureHttpClient(configure =>
    {
        configure.BaseAddress = new(builder.Configuration["ConsumerUrl"]);
    });

var app = builder.Build();

app.Run();
