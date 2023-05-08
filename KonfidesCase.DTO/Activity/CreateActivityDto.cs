namespace KonfidesCase.DTO.Activity
{
    public class CreateActivityDto
    {
        #region Properties
        //public string Organizer { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ActivityDate { get; set; }
        public int Quota { get; set; }
        public string Address { get; set; } = string.Empty;
        //public bool? IsConfirm { get; set; }
        public string? CategoryId { get; set; }
        public string? CityId { get; set; }
        #endregion
    }
}
