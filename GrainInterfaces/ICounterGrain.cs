using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces;

public interface ICounterGrain : IGrainWithStringKey
{
    Task<string> Increment(int increment);
    Task<string> Decrement(int decrement);
    Task<int> GetCount();
}
