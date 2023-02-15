using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GrainInterfaces;

try
{
    using IHost host = await StartClientAsync();
    var client = host.Services.GetRequiredService<IClusterClient>();

    await DoClientWorkAsync(client);
    Console.ReadKey();

    await host.StopAsync();

    return 0;
}
catch (Exception e)
{
    Console.WriteLine($$"""
        Exception while trying to run client: {{e.Message}}
        Make sure the silo is running.
        Press any key to exit.
        """);

    Console.ReadKey();
    return 1;
}

static async Task<IHost> StartClientAsync()
{
    var builder = new HostBuilder()
        .UseOrleansClient(client =>
        {
            client.UseLocalhostClustering();
        })
        .ConfigureLogging(logging => logging.AddConsole());

    var host = builder.Build();
    await host.StartAsync();

    Console.WriteLine("Client successfully connected to silo host.\n");

    return host;
}

static async Task DoClientWorkAsync(IClusterClient client)
{
    var divider = "\n" + new String('-', 50) + "\n";

    // HelloWorld grain
    var helloKey = "Fred";
    var helloGrain = client.GetGrain<IHelloGrain>(helloKey);
    var helloGrainResponse = await helloGrain.SayHello(helloKey);

    Console.WriteLine($"{helloGrainResponse}{divider}");

    // CounterGrain
    var counterGrain = client.GetGrain<ICounterGrain>("Alfread");
    var currentCount = await counterGrain.GetCount();
    Console.WriteLine($"{currentCount}{divider}");

    var result = await counterGrain.Increment(10);
    Console.WriteLine(result);
    currentCount = await counterGrain.GetCount();
    Console.WriteLine($"{currentCount}{divider}");

}
