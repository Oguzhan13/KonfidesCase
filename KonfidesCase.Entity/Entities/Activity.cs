namespace KonfidesCase.Entity.Entities
{
    public class Activity
    {
        #region Properties
        public Guid Id { get; set; }
        public string Organizer { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ActivityDate { get; set; }
        public string Quota { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool? IsConfirm { get; set; }
        public int CategoryId { get; set; }
        public int CityId { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<AppUserActivity>? AttendedUsers { get; set; } = new HashSet<AppUserActivity>();
        public Category Category { get; set; } = new Category();
        public City City { get; set; } = new City();
        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
        #endregion
    }
}
