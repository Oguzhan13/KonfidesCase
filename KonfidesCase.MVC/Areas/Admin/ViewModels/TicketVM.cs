namespace KonfidesCase.MVC.Areas.Admin.ViewModels
{
    public class TicketVM
    {
        public Guid Id { get; set; }
        public string TicketNo { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid ActivityId { get; set; }
    }
}
