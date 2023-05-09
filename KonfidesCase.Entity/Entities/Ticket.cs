namespace KonfidesCase.Entity.Entities
{
    public class Ticket
    {
        #region Constructor
        public Ticket()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string TicketNo { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid ActivityId { get; set; }
        #endregion

        #region Navigation Properties   
        public AppUser? User { get; set; }
        public Activity? Activity { get; set; }
        #endregion
    }
}
