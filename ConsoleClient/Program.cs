using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GrainInterfaces;
using System.Xml.Linq;
using Orleans;

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
        Make sure the silo the client is trying to connect to is running.
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

    Console.WriteLine("Client successfully connected to silo host \n");

    return host;
}

static async Task DoClientWorkAsync(IClusterClient client)
{
    var name = "Jane Bond";
    var divider = new String('-', 50);

    // HelloWorld grain
    var helloGrain = client.GetGrain<IHelloWorldGrain>(name);
    var helloGrainResponse = await helloGrain.SayHelloToAsync(name);

    Console.WriteLine($"\n\n{helloGrainResponse}\n\n{divider}");

    // Person grain
    var personGrain = client.GetGrain<IPersonGrain>(name);
    var personGrainesponse = await personGrain.SayHelloAsync();

    Console.WriteLine($"\n\n{personGrainesponse}\n\n{divider}");

}
