using NotificationService.Client;
using Polly;
using Polly.Retry;

namespace NotificationLibrary.Workshop.Polly;

public static class NotificationLibraryOptionsBuilderExtensions
{
    public static NotificationLibraryOptionsBuilder AddPolly(this NotificationLibraryOptionsBuilder builder, Action<PollyOptionsBuilder> configureOptions)
    {
        var pollyBuilder = new PollyOptionsBuilder();

        configureOptions(pollyBuilder);

        var options = pollyBuilder.Build();

        var retryStrategyOptions = new RetryStrategyOptions
        {
            ShouldHandle = new PredicateBuilder().Handle<NotificationApiException>(),
            BackoffType = options.DelayBackoffType,
            UseJitter = options.UseJitter,
            MaxRetryAttempts = options.MaxRetryCount,
            Delay = options.RetryDelay
        };

        builder.Services.AddResiliencePipeline(PollyConstants.ResiliencePipelineName, builder =>
        {
            builder
                .AddRetry(retryStrategyOptions)
                .AddTimeout(options.Timeout);
        });

        builder.Options.NotificationServiceType = typeof(PollyNotificationService);

        return builder;
    }
}
