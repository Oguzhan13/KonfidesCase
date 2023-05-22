namespace KonfidesCase.MVC.Areas.User.ViewModels
{
    public class TicketVM
    {
        #region Properties
        public Guid Id { get; set; }        
        public string TicketNo { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid ActivityId { get; set; }
        #endregion
    }
}
