namespace KonfidesCase.BLL.Utilities
{
    public class DataResult<TEntity>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public TEntity? Data { get; set; }
    }
}
