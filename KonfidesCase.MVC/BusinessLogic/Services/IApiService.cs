namespace KonfidesCase.MVC.BusinessLogic.Services
{
    public interface IApiService
    {
        Task<string> ApiGetResponse(string urlBaseAddress, string controllerName, string actionName);
        Task<string> ApiPostResponse<TEntity>(TEntity model, string urlBaseAddress, string controllerName, string actionName);
        Task<string> ApiPutResponse<TEntity>(TEntity model, string urlBaseAddress, string controllerName, string actionName);
        void ApiDeserializeResult<TEntity>(string result, out TEntity deserializeModel);
    }
}
