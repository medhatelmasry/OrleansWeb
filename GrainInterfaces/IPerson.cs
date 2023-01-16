using System;
namespace GrainInterfaces;

public interface IPersonGrain : IGrainWithStringKey
{
    Task<string> SayHelloAsync();
}

