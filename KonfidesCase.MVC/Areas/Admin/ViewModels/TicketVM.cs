namespace KonfidesCase.MVC.Areas.Admin.ViewModels
{
    public class TicketVM
    {
        #region Properties
        public Guid Id { get; set; }
        [DisplayName("Bilet No")]
        public string TicketNo { get; set; } = string.Empty;
        [DisplayName("Kullanıcı Id")]
        public Guid UserId { get; set; }
        [DisplayName("Etkinlik Id")]
        public Guid ActivityId { get; set; }
        #endregion
    }
}
