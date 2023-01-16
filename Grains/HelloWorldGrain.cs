using System;
using GrainInterfaces;

namespace Grains;

public class HelloWorldGrain: Grain, IHelloWorldGrain
{
    private int count = 0;

    public Task<string> SayHelloToAsync(string name)
    {
        return Task.FromResult($"Hello {name} from {this.GetPrimaryKeyString()} - called {++count} times");
    }
}

