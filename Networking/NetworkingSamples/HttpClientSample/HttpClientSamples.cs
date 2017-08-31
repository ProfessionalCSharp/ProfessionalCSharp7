using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public class HttpClientSamples : IDisposable
    {
        private const string NorthwindUrl = "http://services.odata.org/Northwind/Northwind.svc/Regions";
        private const string IncorrectUrl = "http://services.odata.org/Northwind1/Northwind.svc/Regions";

        private HttpClient _httpClient;
        public HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient());
        private HttpClient _httpClientWithMessageHandler;
        public HttpClient HttpClientWithMessageHandler => _httpClientWithMessageHandler ?? (_httpClientWithMessageHandler = new HttpClient(new SampleMessageHandler("error")));

        public async Task GetDataSimpleAsync()
        {
            HttpResponseMessage response = await HttpClient.GetAsync(NorthwindUrl);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }
        }

        public async Task GetDataAdvancedAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, NorthwindUrl);

            HttpResponseMessage response = await HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }
        }

        public async Task GetDataWithExceptionsAsync()
        {
            try
            {
                HttpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                ShowHeaders("Request Headers:", HttpClient.DefaultRequestHeaders);
                HttpResponseMessage response = await HttpClient.GetAsync(IncorrectUrl);
                response.EnsureSuccessStatusCode();

                ShowHeaders("Response Headers:", response.Headers);

                Console.WriteLine($"Response Status Code: {response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public async Task GetDataWithHeadersAsync()
        {
            try
            {
                HttpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                ShowHeaders("Request Headers:", HttpClient.DefaultRequestHeaders);

                HttpResponseMessage response = await HttpClient.GetAsync(NorthwindUrl);
                response.EnsureSuccessStatusCode();

                ShowHeaders("Response Headers:", response.Headers);

                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public void ShowHeaders(string title, HttpHeaders headers)
        {
            Console.WriteLine(title);
            foreach (var header in headers)
            {
                string value = string.Join(" ", header.Value);
                Console.WriteLine($"Header: {header.Key} Value: {value}");
            }
            Console.WriteLine();
        }

        public async Task GetDataWithMessageHandlerAsync()
        {
            try
            {
                HttpClientWithMessageHandler.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                ShowHeaders("Request Headers:", HttpClientWithMessageHandler.DefaultRequestHeaders);

                HttpResponseMessage response = await HttpClientWithMessageHandler.GetAsync(NorthwindUrl);
                response.EnsureSuccessStatusCode();

                ShowHeaders("Response Headers:", response.Headers);

                Console.WriteLine($"Response Status Code: {(int)response.StatusCode} {response.ReasonPhrase}");
                string responseBodyAsText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received payload of {responseBodyAsText.Length} characters");
                Console.WriteLine();
                Console.WriteLine(responseBodyAsText);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
            HttpClientWithMessageHandler?.Dispose();
        }
    }
}
