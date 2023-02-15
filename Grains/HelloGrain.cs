using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains;
public class HelloGrain : Grain, IHelloGrain
{
    private readonly ILogger _logger;

    public HelloGrain(ILogger<HelloGrain> logger) => _logger = logger;

    public ValueTask<string> SayHello(string greeting)
    {
        _logger.LogInformation($"SayHello message received: greeting = '{greeting}'");

        return ValueTask.FromResult(
            $"""
            HelloGrain acknowledges receiving message '{greeting}' from client!
            """);

    }
}
