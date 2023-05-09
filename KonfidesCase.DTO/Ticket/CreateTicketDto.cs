namespace KonfidesCase.DTO.Ticket
{
    public class CreateTicketDto
    {
        #region Properties
        public string TicketNo => $"{UserId}-{ActivityId}";
        public Guid? UserId { get; set; }
        public Guid? ActivityId { get; set; }
        #endregion
    }
}
