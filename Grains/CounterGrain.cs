using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains;

// https://codeopinion.com/microsoft-orleans-tutorial-grains-and-silos/

public class CounterGrain : Grain, ICounterGrain
{
    private readonly ILogger _logger;

    public CounterGrain(ILogger<CounterGrain> logger) => _logger = logger;

    private int _counter;

    public Task<string> Increment(int increment)
    {
        _counter += increment;

        var msg = $"Increment message received: ";
        msg += $"counter incremented by '{increment}' to '{_counter}'.";
        _logger.LogInformation(msg);

        msg = $"Grain with KEY={this.GetPrimaryKeyString()} was called";
        msg += $" with request to increment counter by '{increment}'";
        return Task.FromResult(msg);
    }

    public Task<string> Decrement(int decrement)
    {
        _counter -= decrement;

        var msg = $"Decrement message received: ";
        msg += $"counter decremented by '{decrement}' to '{_counter}'.";
        _logger.LogInformation(msg);

        msg = $"Grain with KEY={this.GetPrimaryKeyString()} was called";
        msg += $" with request to decrement counter by '{decrement}'";
        return Task.FromResult(msg);
    }

    public Task<int> GetCount()
    {
        _logger.LogInformation($"GetCount message received: count value = '{_counter}'");

        return Task.FromResult(_counter);
    }
}
