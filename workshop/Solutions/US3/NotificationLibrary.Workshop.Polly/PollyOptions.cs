using Polly;

namespace NotificationLibrary.Workshop.Polly;

public class PollyOptions
{
    internal int MaxRetryCount { get; set; } = 3;
    internal TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(2);
    internal DelayBackoffType DelayBackoffType { get; set; } = DelayBackoffType.Linear;
    internal bool UseJitter { get; set; } = true;
    internal TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);
}
