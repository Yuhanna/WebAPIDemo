﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebApp.Data
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private const string apiName = "ShirtsApi";
        private const string authApiName = "AuthorityApi";
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public WebApiExecuter(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)//appSetting.Development.json da ClientId ve Secret getirme
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<T?> InvokeGet<T>(string relativeURL)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
           // return await httpClient.GetFromJsonAsync<T>(relativeURL);
           var request = new HttpRequestMessage(HttpMethod.Get, relativeURL);
           var response = await httpClient.SendAsync(request);
            await HandlePotentialError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> InvokePost<T>(string   relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
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
            await AddJwtToHeader(httpClient);
            var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
            //response.EnsureSuccessStatusCode();
            await HandlePotentialError(response);
        }
        public async Task InvokeDelete(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var response = await httpClient.DeleteAsync(relativeUrl);
            //response.EnsureSuccessStatusCode();
            await HandlePotentialError(response);
        }

        private async Task HandlePotentialError(HttpResponseMessage httpReponse)
        {
            if(!httpReponse.IsSuccessStatusCode)
            {
                var errorJson = await httpReponse.Content.ReadAsStringAsync();
                throw new WebApiException(errorJson);
            }
        }

        private async Task AddJwtToHeader(HttpClient httpClient)
        {
            JwtToken? token = null;
            string? strToken = httpContextAccessor.HttpContext?.Session.GetString("access_token");
            if(!string.IsNullOrWhiteSpace(strToken))
            {
                token = JsonConvert.DeserializeObject<JwtToken>(strToken);
            }

            if(token == null || token.ExpiresAt <= DateTime.UtcNow)
            {
                var clientId = configuration.GetValue<string>("ClientId");
                var secret = configuration.GetValue<string>("Secret");

                //Authenticate
                var authoClient = httpClientFactory.CreateClient(authApiName);
                var response = await authoClient.PostAsJsonAsync("auth", new AppCredential
                {
                    ClientId = clientId,
                    Secret = secret
                });
                response.EnsureSuccessStatusCode();//if not success throwing an exception


                //Get the JWT
                strToken = await response.Content.ReadAsStringAsync();//token in format of Postman programı
                token = JsonConvert.DeserializeObject<JwtToken>(strToken);

                httpContextAccessor.HttpContext?.Session.SetString("access_token", strToken);
            }

            //Press the JWT to endpoints through the http headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);
        }
    }
}
