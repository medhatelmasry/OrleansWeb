using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers;

public class HelloWorldController : Controller
{
    private readonly IClusterClient clusterClient;

    public HelloWorldController(IClusterClient clusterClient)
    {
        this.clusterClient = clusterClient;
    }

    [HttpGet("/hello/{name}")]
    public async Task<IActionResult> Hello(string name)
    {
        var result = await clusterClient.GetGrain<IHelloWorldGrain>(name).SayHelloToAsync(name);
        return Ok(result);
    }

    [HttpGet("/person/{name}")]
    public async Task<IActionResult> Person(string name)
    {
        var result = await clusterClient.GetGrain<IPersonGrain>(name).SayHelloAsync();
        return Ok(result);
    }
}

