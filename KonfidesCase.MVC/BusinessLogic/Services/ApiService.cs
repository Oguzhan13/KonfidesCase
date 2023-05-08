﻿using Newtonsoft.Json;
using System.Text;

namespace KonfidesCase.MVC.BusinessLogic.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> ApiPostResponse<TEntity>(TEntity model, string urlBaseAddress, string controllerName, string actionName)
        {
            HttpClient client = _httpClientFactory.CreateClient(urlBaseAddress);
            var serializeModel = JsonConvert.SerializeObject(model);
            var data = new StringContent(serializeModel, Encoding.UTF8, "application/json");
            var responseApi = await client.PostAsync($"{controllerName}/{actionName}", data);
            return await responseApi.Content.ReadAsStringAsync();
        }

        public void ApiDeserializeResult<TEntity>( string result, out TEntity deserializeModel)
        {
            deserializeModel = JsonConvert.DeserializeObject<TEntity>(result)!;
        }
    }
}