using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactoryService _http; //Is communicating with the database through the API (gives the orders to API on behalf of our application)
        public ApiService(IHttpClientFactoryService http)
        {
            _http = http;
        }

        private string GetUri<TEntity>() => $"api/{typeof(TEntity).Name}s"; //Generics - generating a search address for any entity - Api address- Type Class- Name- Course- Id-2
        private string GetUri<TEntity>(int id) => $"api/{typeof(TEntity).Name}s/{id}"; //Same but with the id (for update, delete and get specific one)

        public async Task<List<TDto>> GetAsync<TEntity, TDto>(bool include = false)
            where TEntity : class
            where TDto : class
        {
            try
            {
                var uri = GetUri<TEntity>();
                return await _http.GetAsync<TDto>($"{uri}?include={include.ToString()}", "AdminClient");
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TDto> SingleAsync<TEntity, TDto>(int id, bool include = false)
            where TEntity : class
            where TDto : class
        {
            try
            {
                var uri = GetUri<TEntity>(id);
                var result = await _http.SingleAsync<TDto>($"{uri}?include={include.ToString()}", "AdminClient");
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CreateAsync<TEntity, TDto>(TDto item)
            where TEntity : class
            where TDto : class
        {
            try
            {
                var uri = GetUri<TEntity>();
                var response = await _http.PostAsync<TDto, TDto>(item, uri, "AdminClient");
                return (int)response.GetType().GetProperty("Id").GetValue(response);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateAsync<TEntity, TDto>(TDto item)
            where TEntity : class
            where TDto : class
        {
            try
            {
                var id = (int)item.GetType().GetProperty("Id").GetValue(item); //GetType- specify the object, GetProperty-specify the property and then change the value of it- Course-Id-2
                var uri = GetUri<TEntity>(id);

                var response = await _http.PutAsync<TDto, TDto>(item, uri, "AdminClient");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class
        {
            try
            {
                var uri = GetUri<TEntity>(id);

                var response = await _http.DeleteAsync(uri, "AdminClient");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
