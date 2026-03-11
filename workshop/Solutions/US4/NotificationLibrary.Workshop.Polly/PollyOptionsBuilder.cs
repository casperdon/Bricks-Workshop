using Polly;

namespace NotificationLibrary.Workshop.Polly;

public class PollyOptionsBuilder
{
    private readonly PollyOptions _options = new PollyOptions();

    public PollyOptionsBuilder WithMaxRetryCount(int maxRetryCount)
    {
        _options.MaxRetryCount = maxRetryCount;
        return this;
    }

    public PollyOptionsBuilder WithRetryDelay(TimeSpan retryDelay)
    {
        _options.RetryDelay = retryDelay;
        return this;
    }

    public PollyOptionsBuilder WithDelayBackoffType(DelayBackoffType delayBackoffType)
    {
        _options.DelayBackoffType = delayBackoffType;
        return this;
    }

    public PollyOptionsBuilder WithUseJitter(bool useJitter)
    {
        _options.UseJitter = useJitter;
        return this;
    }

    public PollyOptionsBuilder WithTimeout(TimeSpan timeout)
    {
        _options.Timeout = timeout;
        return this;
    }

    public PollyOptions Build() => _options;
}
