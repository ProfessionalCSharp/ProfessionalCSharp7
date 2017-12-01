using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSampleApp.Services
{
    public class SampleService : ISampleService
    {
        public IEnumerable<string> GetSampleStrings() => new[] { "one", "two", "three", "four" };
    }
}
