using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ASPNETCoreSample.IntegrationTest
{
    public class ASPNETCoreSampleTest : IDisposable
    {
        private TestServer _testServer;

        public ASPNETCoreSampleTest()
        {
            _testServer = new TestServer(
                WebHost.CreateDefaultBuilder().UseStartup<Startup>());
        }

        public void Dispose() => _testServer?.Dispose();

        [Fact]
        public async Task ReturnHelloWorld()
        {
            // act
            RequestBuilder requestBuilder = _testServer.CreateRequest("/");
            HttpResponseMessage response = await requestBuilder.GetAsync();
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // assert
            Assert.Equal("Hello World!", responseString);
        }
    }
}
