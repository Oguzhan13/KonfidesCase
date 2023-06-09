﻿namespace KonfidesCase.MVC.Areas.User.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Organizer { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ActivityDate { get; set; }
        public int Quota { get; set; }
        public string Address { get; set; } = string.Empty;
        public bool? IsConfirm { get; set; }
        public int CategoryId { get; set; }
        public int CityId { get; set; }
    }
}
