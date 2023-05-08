namespace KonfidesCase.Entity.Entities
{
    public class Activity
    {
        #region Constructor
        public Activity()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string Organizer { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ActivityDate { get; set; }
        public int Quota { get; set; }
        public string Address { get; set; } = string.Empty;
        public bool? IsConfirm { get; set; }
        public int? CategoryId { get; set; }
        public int? CityId { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<AppUserActivity>? AttendedUsers { get; set; }
        public Category? Category { get; set; }
        public City? City { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
        #endregion
    }
}
