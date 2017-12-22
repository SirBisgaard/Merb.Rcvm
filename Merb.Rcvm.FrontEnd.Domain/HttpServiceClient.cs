using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Merb.Rcvm.FrontEnd.Domain
{
    public class HttpServiceClient<T> where T : class
    {
        private HttpClient _client;

        public HttpServiceClient(string baseAddress)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<T>> GetCollection(string path = "")
        {
            HttpResponseMessage response = await _client.GetAsync(path);
            if (!response.IsSuccessStatusCode)
                return new T[0];

            return await response.Content.ReadAsAsync<IEnumerable<T>>();
        }

        public async Task<T> Get(string id, string path = "")
        {
            HttpResponseMessage response = await _client.GetAsync($"{path}{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task Create(T value, string path = "")
        {
            await _client.PostAsJsonAsync(path, value);
        }

        public async Task Update(T value, string path = "")
        {
            await _client.PutAsJsonAsync(path, value);
        }

        public async Task Delete(string id, string path = "")
        {
            await _client.DeleteAsync(path + id);
        }
    }
}
