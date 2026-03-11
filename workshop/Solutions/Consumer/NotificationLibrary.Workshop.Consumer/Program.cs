using NotificationLibrary.Workshop;
using NotificationLibrary.Workshop.Email;
using NotificationLibrary.Workshop.Polly;
using NotificationLibrary.Workshop.Queue;
using NotificationLibrary.Workshop.Queue.InMemory;
using NotificationLibrary.Workshop.Queue.RabbitMQ;
using NotificationLibrary.Workshop.Sms;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// US1
/*
builder.Services.AddNotifications(builder.Configuration, options =>
{
    options.WithNotificationServiceLifetime(ServiceLifetime.Scoped)
           .ConfigureApiAccess("Notifications");
});
*/

// US2
/*
builder.Services.AddNotifications(builder.Configuration, options =>
{
    options.WithNotificationServiceLifetime(ServiceLifetime.Scoped)
           .ConfigureApiAccess("Notifications")
           .AddPolly(p => p
                .WithMaxRetryCount(3)
                .WithUseJitter(true)
                .WithTimeout(TimeSpan.FromSeconds(30))
                .WithRetryDelay(TimeSpan.FromSeconds(5))
                .WithDelayBackoffType(DelayBackoffType.Exponential));
});
*/

// US3
/*
builder.Services.AddNotifications(builder.Configuration, options =>
{
    options.WithNotificationServiceLifetime(ServiceLifetime.Scoped)
           .ConfigureApiAccess("Notifications")
           .AddPolly(p => p
                .WithMaxRetryCount(3)
                .WithUseJitter(true)
                .WithTimeout(TimeSpan.FromSeconds(30))
                .WithRetryDelay(TimeSpan.FromSeconds(5))
                .WithDelayBackoffType(DelayBackoffType.Exponential))
           .AddSms()
           .AddEmail();
});
*/

// US4
builder.Services.AddNotifications(builder.Configuration, options =>
{
    options.WithNotificationServiceLifetime(ServiceLifetime.Scoped)
           .ConfigureApiAccess("Notifications")
           .AddPolly(p => p
                .WithMaxRetryCount(3)
                .WithUseJitter(true)
                .WithTimeout(TimeSpan.FromSeconds(30))
                .WithRetryDelay(TimeSpan.FromSeconds(5))
                .WithDelayBackoffType(DelayBackoffType.Exponential))
           .AddSms()
           .AddEmail()
           .AddMessageBus(mb => mb
                  .UseInMemoryQueue());
                /*.UseRabbitMQQueue(rabbit => rabbit
                    .WithHostName("localhost")
                    .WithPort(5672)
                    .WithUserName("guest")
                    .WithPassword("guest")
                    .WithVirtualHost("/")
                    .WithQueueName("notifications")));*/
});

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
