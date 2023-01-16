using System;
using GrainInterfaces;

namespace Grains;

	public class PersonGrain : Grain, IPersonGrain
	{
    private int id;
		public PersonGrain()
		{
        id = new Random().Next();
		}

    public Task<string> SayHelloAsync()
    {
        return Task.FromResult($"Response from grain ID = {this.id} and KEY = {this.GetPrimaryKeyString()}");
    }
}

