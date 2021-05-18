using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Extensions;

namespace VOD.Common.Exceptions
{
    public static class HttpClientExtensions
    {
        //Helper methods 
        private static HttpRequestMessage CreateRequestHeaders(this string uri, HttpMethod httpMethod) //Properties to add to our request
        {
            var requestHeader = new HttpRequestMessage(httpMethod, uri);
            requestHeader.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //Text format being received in Json
            if (httpMethod.Equals(HttpMethod.Get)) requestHeader.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            return requestHeader;
        }

        private static async Task CheckStatusCodes(this HttpResponseMessage response) //Ensuring that the response status is ok
        {
            if (!response.IsSuccessStatusCode)
            {
                object validationErrors = null;
                var message = string.Empty;
                if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
                {
                    var errorStream = await response.Content.ReadAsStreamAsync();
                    validationErrors = errorStream.ReadAndDeserializeFromJson();
                    message = "Could not process the entity";
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                    message = "Bad request.";
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                    message = "Access denied.";
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    message = "Could not find the entity.";
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    message = "Not logged in.";

                throw new HttpResponseException(response.StatusCode, message, validationErrors);

            }

            else response.EnsureSuccessStatusCode();
        }

        public static object ReadAndDeserializeFromJson(this Stream stream) //To be able to read json response 
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (!stream.CanRead) throw new NotSupportedException
            (
                "Can't read from this stream."
            );

            using (var streamReader = new StreamReader(stream, new UTF8Encoding(), true, 1024, false)) //Format for receiving Json data 
            {
                using (var jsonTextReader = new JsonTextReader(streamReader)) //Turning stream data(binary data) to json data so we can read
                {
                    var jsonSerializer = new JsonSerializer();
                    return jsonSerializer.Deserialize(jsonTextReader); //From bytes to string and reverse/ Serialization can convert these complex objects into byte strings for such use. After the byte strings are transmitted, the receiver will have to recover the original object from the byte string. This is known as deserialization.
                }
            }

        }
        public static T ReadAndDeserializeFromJson<T>(this Stream stream) //Same method but for generics
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (!stream.CanRead) throw new NotSupportedException
            (
                "Can't read from this stream."
            );

            using (var streamReader = new StreamReader(stream, new UTF8Encoding(), true, 1024, false)) //Format for receiving Json data 
            {
                using (var jsonTextReader = new JsonTextReader(streamReader)) //Turning stream data(binary data) to json data so we can read
                {
                    var jsonSerializer = new JsonSerializer();
                    return jsonSerializer.Deserialize<T>(jsonTextReader); //Returning a list of Course DTOs

                }
            }

        }
       
        private static async Task<HttpRequestMessage> CreateRequestContent<TRequest>(this HttpRequestMessage requestMessage, TRequest content)
        {
            requestMessage.Content = await content.SerializeRequestContentAsync();
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return requestMessage;
        }

        private static async Task<StreamContent> SerializeRequestContentAsync<TRequest>(this TRequest content)
        {
            var stream = new MemoryStream();
            await stream.SerializeToJsonAndWriteAsync(content, new UTF8Encoding(), 1024, true);
            stream.Seek(0, SeekOrigin.Begin);
            return new StreamContent(stream);
        }

        private static async Task<TResponse> DeserializeResponse<TResponse>(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return responseStream.ReadAndDeserializeFromJson<TResponse>();
        }
        public static async Task<List<TResponse>> GetAsync<TResponse>(this HttpClient client, string uri)
        {
            try
            {
                var requestMessage = uri.CreateRequestHeaders(HttpMethod.Get);
                using (var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    await response.CheckStatusCodes();
                    return stream.ReadAndDeserializeFromJson<List<TResponse>>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<TResponse> GetAsync<TResponse, TRequest>(this HttpClient client, string uri, TRequest content )
        {
            try
            {
                var requestMessage = uri.CreateRequestHeaders(HttpMethod.Get);
                if (content != null) await requestMessage.CreateRequestContent(content);

                using (var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    await response.CheckStatusCodes();
                    return stream.ReadAndDeserializeFromJson<TResponse>();
                }
            }

            catch
            {
                throw;

            }

        }

        public static async Task<TResponse> PostAsync<TRequest, TResponse>(this HttpClient client, string uri, TRequest content)
        {
            try
            {
                using (var requestMessage = uri.CreateRequestHeaders(HttpMethod.Post))
                {
                    using ((await requestMessage.CreateRequestContent(content)).Content)
                    { 
                        using (var responseMessage = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
                        {
                            await responseMessage.CheckStatusCodes();
                            return await responseMessage.DeserializeResponse<TResponse>();
                        }
                    }
                }
            }

            catch
            {
                throw;

            }

        }
        public static async Task<TResponse> PutAsync<TRequest, TResponse>(this HttpClient client, string uri, TRequest content)
        {
            try
            {
                using (var requestMessage = uri.CreateRequestHeaders(HttpMethod.Put))
                {
                    using ((await requestMessage.CreateRequestContent(content)).Content)
                    {
                        using (var responseMessage = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
                        {
                            await responseMessage.CheckStatusCodes();
                            return await responseMessage.DeserializeResponse<TResponse>();
                        }
                    }
                }
            }

            catch
            {
                throw;

            }

        }
        public static async Task<string> RemoveAsync(this HttpClient client, string uri)
        {
            try
            {
                using (var requestMessage = uri.CreateRequestHeaders(HttpMethod.Delete))
                {
                   
                        using (var responseMessage = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
                        {
                            await responseMessage.CheckStatusCodes();
                            return await responseMessage.Content.ReadAsStringAsync();
                        }
                }
            }

            catch
            {
                throw;

            }

        }

    }
}