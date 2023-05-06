namespace KonfidesCase.Entity.Entities
{
    public class Ticket
    {
        #region Properties
        public Guid Id { get; set; }
        public string TicketNo { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
        public Guid? ActivityId { get; set; }
        #endregion

        #region Navigation Properties   
        public AppUser User { get; set; } = new AppUser();
        public Activity Activity { get; set; } = new Activity();
        #endregion
    }
}
