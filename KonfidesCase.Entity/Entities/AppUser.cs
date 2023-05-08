namespace KonfidesCase.Entity.Entities
{
    public class AppUser
    {
        #region Properties
        public Guid Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        #endregion

        #region Navigation Properties
        public ICollection<AppUserActivity>? Activities { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
        #endregion
    }
}
