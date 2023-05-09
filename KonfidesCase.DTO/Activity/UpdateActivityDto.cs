namespace KonfidesCase.DTO.Activity
{
    public class UpdateActivityDto
    {
        public Guid Id { get; set; }
        public int Quota { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
