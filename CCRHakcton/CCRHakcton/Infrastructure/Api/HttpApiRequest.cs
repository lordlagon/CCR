using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public interface IHttpApiRequest
    {
        Task<TResponse> PostAsync<TResponse>(string requestUri, object body, Dictionary<string, string> headers = null);
        Task PostAsync(string requestUri, object body, Dictionary<string, string> headers = null);
        Task PatchAsync(string requestUri, object body, Dictionary<string, string> headers = null);
        Task<TResponse> DeleteAsync<TResponse>(string requestUri, Dictionary<string, string> headers = null);
        Task<TResponse> PutAsync<TResponse>(string requestUri, object body, Dictionary<string, string> headers = null);
        Task PutAsync(string requestUri, object body, Dictionary<string, string> headers = null);
        Task<TResponse> GetAsync<TResponse>(string requestUri, Dictionary<string, string> headers = null);
    }

    public sealed class HttpApiRequest : IHttpApiRequest
    {
        readonly HttpClient HttpClient;

        public HttpApiRequest()
        {
            HttpClient = new HttpClient();
        }

        public Task<TResponse> PostAsync<TResponse>(string requestUri, object body, Dictionary<string, string> headers = null)
            => RestAuxAsync<TResponse>(HttpMethod.Post, requestUri, body, headers);

        public Task PostAsync(string requestUri, object body, Dictionary<string, string> headers = null)
            => RestAuxAsync(HttpMethod.Post, requestUri, body, headers);

        public Task PatchAsync(string requestUri, object body, Dictionary<string, string> headers = null)
            => RestAuxAsync(new HttpMethod("PATCH"), requestUri, body, headers);

        public Task<TResponse> DeleteAsync<TResponse>(string requestUri, Dictionary<string, string> headers = null)
            => RestAuxAsync<TResponse>(HttpMethod.Delete, requestUri, headers: headers);

        public Task<TResponse> PutAsync<TResponse>(string requestUri, object body, Dictionary<string, string> headers = null)
            => RestAuxAsync<TResponse>(HttpMethod.Put, requestUri, body, headers);

        public Task PutAsync(string requestUri, object body, Dictionary<string, string> headers = null)
            => RestAuxAsync(HttpMethod.Put, requestUri, body, headers);

        public Task<TResponse> GetAsync<TResponse>(string requestUri, Dictionary<string, string> headers = null)
            => RestAuxAsync<TResponse>(HttpMethod.Get, requestUri, headers: headers);

        async Task<TResponse> RestAuxAsync<TResponse>(HttpMethod httpMethod, string requestUri, object body, Dictionary<string, string> headers = null)
        {
            if (body is Stream streamContent)
            {
                using (var content = new StreamContent(streamContent))
                    return await RestAuxAsync<TResponse>(httpMethod, requestUri, content, headers);
            }
            else
            {
                var json = JsonConvert.SerializeObject(body, new CustomJsonSerializerSettings());
                string json2 = json;
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                    return await RestAuxAsync<TResponse>(httpMethod, requestUri, content, headers);
            }
        }

        async Task RestAuxAsync(HttpMethod httpMethod, string requestUri, object body, Dictionary<string, string> headers = null)
        {
            if (body is Stream streamContent)
            {
                using (var content = new StreamContent(streamContent))
                    await RestAuxAsync(httpMethod, requestUri, content, headers);
            }
            else
            {
                var json = JsonConvert.SerializeObject(body, new CustomJsonSerializerSettings());
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                    await RestAuxAsync(httpMethod, requestUri, content, headers);
            }
        }

        async Task<TResponse> RestAuxAsync<TResponse>(HttpMethod httpMethod, string requestUri, HttpContent requestContent = null, Dictionary<string, string> headers = null)
            => (TResponse)await RestAuxAsync(typeof(TResponse), httpMethod, requestUri, requestContent, headers);

        Task RestAuxAsync(HttpMethod httpMethod, string requestUri, HttpContent requestContent = null, Dictionary<string, string> headers = null)
            => RestAuxAsync(null, httpMethod, requestUri, requestContent, headers);

        async Task<object> RestAuxAsync(Type returnType, HttpMethod httpMethod, string requestUri, HttpContent requestContent = null, Dictionary<string, string> headers = null)
        {
            using (var request = new HttpRequestMessage(httpMethod, $"{AppConfiguration.ApiUrl}{requestUri}"))
            {
                if (headers == null)
                    headers = new Dictionary<string, string>();

                headers.Add("Accept", "application/json");
                headers.Add("Culture", Thread.CurrentThread.CurrentUICulture.Name);

                foreach (var item in headers)
                    request.Headers.Add(item.Key, item.Value);

                request.Content = requestContent;

                using (var response = await HttpClient.SendAsync(request).ConfigureAwait(false))
                using (var content = response.Content)
                {
                    if (returnType?.Name == typeof(Stream).Name)
                    {
                        using(var stream = await content.ReadAsStreamAsync())
                        {
                            MemoryStream ms = new MemoryStream();
                            stream.CopyTo(ms);
                            ms.Position = 0;
                            return ms;
                        }
                    }

                    var result = await content.ReadAsStringAsync();

                  

                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            if (returnType != null)
                            {
                                var json = JsonConvert.DeserializeObject(result, returnType);
                                return json;
                            }
                            return null;
                        case System.Net.HttpStatusCode.NoContent:
                            return null;
                    }

                    throw new ApiException(result)
                    {
                        StatusCode = response.StatusCode,
                        Content = result,
                    };
                }
            }
        }
    }

    public class CustomJsonSerializerSettings : JsonSerializerSettings
    {
        public CustomJsonSerializerSettings()
        {
            Formatting = Formatting.Indented;
            ContractResolver = new DictionaryAsArrayResolver();
        }
    }

    class DictionaryAsArrayResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            if (objectType.GetInterfaces().Any(i => i == typeof(IDictionary) ||
                (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>))))
            {
                return base.CreateArrayContract(objectType);
            }

            return base.CreateContract(objectType);
        }
    }
}
