using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Exceptions;
using VOD.Common.Extensions;

namespace VOD.Common.Services
{
    public class HttpClientFactoryService : IHttpClientFactoryService
    {       
        //Properties
        private readonly IHttpClientFactory _httpClientFactory; //Microsoft's own tool with configurations for creating HttpClinet instances

        //Constructor
        public HttpClientFactoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<TResponse>> GetAsync<TResponse>(string uri, string serviceName) where TResponse : class
        {
            try
            {
                if (uri.IsEmpty() || serviceName.IsEmpty())
                    throw new HttpResponseException(HttpStatusCode.NotFound, "Could not find the resource.");
                var httpClient = _httpClientFactory.CreateClient(serviceName);
                return await httpClient.GetAsync<TResponse>(uri.ToLower());
            }

            catch
            {
                throw;
            }
        }
        public async Task<TResponse> SingleAsync<TResponse>(string uri, string serviceName) where TResponse : class
        {
            try
            {
                if (uri.IsEmpty() || serviceName.IsEmpty())
                    throw new HttpResponseException(HttpStatusCode.NotFound, "Could not find the resource.");
                var httpClient = _httpClientFactory.CreateClient(serviceName);
                return await httpClient.GetAsync<TResponse, string>(uri.ToLower(), null);
            }

            catch
            {
                throw;
            }
        }
        public async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest content, string uri, string serviceName) where TRequest : class where TResponse : class
        {
            try
            {
                if (uri.IsEmpty() || serviceName.IsEmpty())
                    throw new HttpResponseException(HttpStatusCode.NotFound, "Could not find the resource.");
                var httpClient = _httpClientFactory.CreateClient(serviceName);
                return await httpClient.PostAsync<TRequest, TResponse>(uri.ToLower(), content);
            }

            catch
            {
                throw;
            }
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(TRequest content, string uri, string serviceName)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                if (uri.IsEmpty() || serviceName.IsEmpty())
                    throw new HttpResponseException(HttpStatusCode.NotFound, "Could not find the resource.");
                var httpClient = _httpClientFactory.CreateClient(serviceName);
                return await httpClient.PutAsync<TRequest, TResponse>(uri.ToLower(), content);
            }

            catch
            {
                throw; 
            }
        }

        public async Task<string> DeleteAsync(string uri, string serviceName)
        {
            try
            {
                if (uri.IsEmpty() || serviceName.IsEmpty())
                    throw new HttpResponseException(HttpStatusCode.NotFound, "Could not find the resource.");

                var httpClient = _httpClientFactory.CreateClient(serviceName); //Creating the HttpClient that will communicate with the API

                return await httpClient.RemoveAsync(uri.ToLower());
            }

            catch
            {
                throw;
            }
        }
    }
}
