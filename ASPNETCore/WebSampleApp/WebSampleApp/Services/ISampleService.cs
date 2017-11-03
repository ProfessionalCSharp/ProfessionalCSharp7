using System.Collections.Generic;

namespace WebSampleApp.Services
{
    public interface ISampleService
    {
        IEnumerable<string> GetSampleStrings();
    }
}
