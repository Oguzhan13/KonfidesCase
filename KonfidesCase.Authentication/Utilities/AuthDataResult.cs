namespace KonfidesCase.Authentication.Utilities
{
    public class AuthDataResult<TEntity>
    {
        #region Properties
        public bool IsSuccess { get; set; } = false;
        public string? Message { get; set; }
        public TEntity? Data { get; set; }
        #endregion
    }
}
