using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.Services
{
   public interface IHttpClientFactoryService
    {
        Task<List<TResponse>> GetAsync<TResponse>(string uri, string serviceName) where TResponse : class; //Response since it is a web response from the API (receiving an entity still though), string uri- the address we receive, serviceName- AdminClient service we are using from Startup
        Task<TResponse> SingleAsync<TResponse> (string uri, string serviceName) where TResponse : class;
        Task<TResponse> PostAsync<TRequest, TResponse>(TRequest content, string uri, string serviceName) where TRequest : class where TResponse : class;
        Task<TResponse> PutAsync<TRequest, TResponse>(TRequest content, string uri, string serviceName) where TRequest : class where TResponse : class;
        Task<string> DeleteAsync(string uri, string serviceName);
    }
}
