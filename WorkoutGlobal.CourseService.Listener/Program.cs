using WorkoutGlobal.CourseService.Listener.Extensions.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

var busType = Enum.Parse<WorkoutGlobal.CourseService.Listener.Enums.Bus>(
    value: builder.Configuration["MassTransitSettings:Bus"]);

builder.Services.ConfigureMassTransit(builder.Configuration, busType);
builder.Services.ConfigureRefitServices(builder.Configuration);

var app = builder.Build();

app.Run();
