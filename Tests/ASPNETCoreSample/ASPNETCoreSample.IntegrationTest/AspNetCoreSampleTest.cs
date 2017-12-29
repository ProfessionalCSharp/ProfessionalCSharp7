using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ASPNETCoreSample.IntegrationTest
{
    public class ASPNETCoreSampleTest
    {
        private TestServer _testServer;
        private HttpClient _httpClient;

        public ASPNETCoreSampleTest()
        {
            _testServer = new TestServer(
                WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            _httpClient = _testServer.CreateClient();
        }

        [Fact]
        public async Task ReturnHelloWorld()
        {
            // act
            HttpResponseMessage response = await _httpClient.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // assert
            Assert.Equal("Hello World!", responseString);
        }

        [Fact]
        public async Task ReturnHelloWorld2()
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
