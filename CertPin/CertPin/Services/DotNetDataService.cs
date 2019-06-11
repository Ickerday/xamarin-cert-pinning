using HttpTracer;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Android.Net;
using Xamarin.Forms;

namespace CertPin.Services
{
    public class DotNetDataService
    {
        public Task<string> GetPinnedDataAsync() => GetDataAsync(App.BASE_URL);

        private async Task<string> GetDataAsync(string baseUrl)
        {
            var client = GetHttpClient(baseUrl);

            string result = string.Empty;
            try
            {
                var response = await client.GetAsync(baseUrl, HttpCompletionOption.ResponseHeadersRead);
                result = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            Debug.WriteLine(result);

            return result;
        }

        private HttpClient GetHttpClient(string baseUrl)
        {
            var handler = Device.RuntimePlatform == Device.Android
                ? DependencyService.Get<AndroidClientHandler>() :
                new HttpClientHandler();

            handler.UseProxy = true;
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            var tracerHandler = new HttpTracerHandler(handler);

            var client = new HttpClient(tracerHandler) { BaseAddress = new Uri(baseUrl) };

            client.DefaultRequestHeaders.Connection.Add("keep-alive");
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

            return client;
        }
    }
}
