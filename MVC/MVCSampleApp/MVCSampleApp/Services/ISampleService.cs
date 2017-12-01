using System.Collections.Generic;

namespace MVCSampleApp.Services
{
    public interface ISampleService
    {
        IEnumerable<string> GetSampleStrings();
    }
}