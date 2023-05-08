namespace KonfidesCase.MVC.BusinessLogic.Services
{
    public interface IApiService
    {
        Task<string> ApiPostResponse<TEntity>(TEntity model, string urlBaseAddress, string controllerName, string actionName);
        void ApiDeserializeResult<TEntity>(string result, out TEntity deserializeModel);
    }
}
