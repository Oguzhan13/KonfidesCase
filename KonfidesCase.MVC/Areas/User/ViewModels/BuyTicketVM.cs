namespace KonfidesCase.MVC.Areas.User.ViewModels
{
    public class BuyTicketVM
    {
        public string TicketNo => $"{UserId}-{ActivityId}";
        public Guid UserId { get; set; }
        public Guid ActivityId { get; set; }
    }
}
