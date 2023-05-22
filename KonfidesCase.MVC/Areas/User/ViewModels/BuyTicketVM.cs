namespace KonfidesCase.MVC.Areas.User.ViewModels
{
    public class BuyTicketVM
    {
        #region Properties
        [DisplayName("Bilet No")]
        public string TicketNo => $"{UserId}-{ActivityId}";        
        public Guid UserId { get; set; }        
        public Guid ActivityId { get; set; }
        #endregion
    }
}
