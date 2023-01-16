using System;
namespace GrainInterfaces;

public interface IHelloWorldGrain: IGrainWithStringKey
{
    Task<string> SayHelloToAsync(string name);
}

