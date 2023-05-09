using Newtonsoft.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KonfidesCase.MVC.BusinessLogic.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> ApiGetResponse(string urlBaseAddress, string controllerName, string actionName)
        {
            HttpClient client = _httpClientFactory.CreateClient(urlBaseAddress);
            var responseApi = await client.GetAsync($"{controllerName}/{actionName}");
            return await responseApi.Content.ReadAsStringAsync();
        }

        public async Task<string> ApiPostResponse<TEntity>(TEntity model, string urlBaseAddress, string controllerName, string actionName)
        {
            HttpClient client = _httpClientFactory.CreateClient(urlBaseAddress);
            var serializeModel = JsonConvert.SerializeObject(model);
            var data = new StringContent(serializeModel, Encoding.UTF8, "application/json");
            var responseApi = await client.PostAsync($"{controllerName}/{actionName}", data);
            return await responseApi.Content.ReadAsStringAsync();
        }

        public async Task<string> ApiPutResponse<TEntity>(TEntity model, string urlBaseAddress, string controllerName, string actionName)
        {
            HttpClient client = _httpClientFactory.CreateClient(urlBaseAddress);
            var serializeModel = JsonConvert.SerializeObject(model);
            var data = new StringContent(serializeModel, Encoding.UTF8, "application/json");
            var responseApi = await client.PutAsync($"{controllerName}/{actionName}", data);
            return await responseApi.Content.ReadAsStringAsync();
        }
    }
}
