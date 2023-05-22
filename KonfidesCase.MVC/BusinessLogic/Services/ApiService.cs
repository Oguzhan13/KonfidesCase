using System.Text;

namespace KonfidesCase.MVC.BusinessLogic.Services
{
    public class ApiService : IApiService
    {
        #region Field & Constructor
        private readonly IHttpClientFactory _httpClientFactory;
        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        #region Methods for API project responses
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
        #endregion
    }
}
