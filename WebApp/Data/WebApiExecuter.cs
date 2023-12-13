using System.Net.Http;
using System.Text.Json;

namespace WebApp.Data
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private const string apiName = "ShirtsApi";
        private readonly IHttpClientFactory httpClientFactory;

        public WebApiExecuter(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<T?> InvokeGet<T>(string relativeURL)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
           // return await httpClient.GetFromJsonAsync<T>(relativeURL);
           var request = new HttpRequestMessage(HttpMethod.Get, relativeURL);
           var response = await httpClient.SendAsync(request);
            await HandlePotentialError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> InvokePost<T>(string   relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);
            //response.EnsureSuccessStatusCode();
           /* if(!response.IsSuccessStatusCode)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                throw new WebApiException(errorJson);
            }*/
           await HandlePotentialError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task InvokePut<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
            //response.EnsureSuccessStatusCode();
            await HandlePotentialError(response);
        }
        public async Task InvokeDelete(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.DeleteAsync(relativeUrl);
            //response.EnsureSuccessStatusCode();
            await HandlePotentialError(response);
        }

        private async Task HandlePotentialError(HttpResponseMessage httpReponse)
        {
            if(!httpReponse.IsSuccessStatusCode)
            {
                var errorJson = await httpReponse.Content.ReadAsStringAsync();
                throw new Exception(errorJson);
            }
        }
    }
}
