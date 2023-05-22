namespace KonfidesCase.DTO.Activity
{
    public class CreateActivityDto
    {
        #region Properties        
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ActivityDate { get; set; }
        public int Quota { get; set; }
        public string Address { get; set; } = string.Empty;        
        public int CategoryId { get; set; }
        public int CityId { get; set; }
        #endregion
    }
}
