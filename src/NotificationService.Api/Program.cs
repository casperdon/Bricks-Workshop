using NotificationService.Api.Authentication;
using NotificationService.Api.Endpoints;
using NotificationService.Api.Store;
using NSwag;
using NSwag.Generation.Processors.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApiDocument((options, s) =>
{
    options.Title = "Notification Api";
    options.Version = "v1.0";

    options.AddSecurity("ApiKey", Enumerable.Empty<string>(), new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "X-Api-Key",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "API Key needed to access the endpoints. Header key: X-Api-Key"
    });

    options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("ApiKey"));
});
builder.Services.AddSingleton<INotificationStore, InMemoryNotificationStore>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseApiKeySecurity();
app.MapNotificationEndpoints();

app.Run();
