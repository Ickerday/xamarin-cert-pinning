using HttpTracer;
using Refit;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Android.Net;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Preserve]
namespace CertPin.Services
{
    [Headers("Accept-Encoding: gzip, deflate", "Connection: keep-alive")]
    public interface IDataService
    {
        [Get("/")]
        Task<string> GetDataAsync();
    }

    public class RefitDataService
    {
        private readonly IDataService _certService;

        public RefitDataService() =>
            _certService = RestService.For<IDataService>(GetHttpClient());

        public async Task<string> GetDataAsync()
        {
            var result = await _certService.GetDataAsync();

            Debug.WriteLine(result);

            return result;
        }

        private HttpClient GetHttpClient()
        {
            var handler = Device.RuntimePlatform == Device.Android
                ? DependencyService.Get<AndroidClientHandler>()
                : new HttpClientHandler();

            handler.UseProxy = true;
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            var tracerHandler = new HttpTracerHandler(handler);
            return new HttpClient(tracerHandler) { BaseAddress = new Uri(App.BASE_URL) };
        }

    }
}
