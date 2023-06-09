﻿namespace KonfidesCase.Entity.Entities
{
    public class City
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        #endregion

        #region Navigation Property
        public ICollection<Activity>? Activities { get; set; }
        #endregion
    }
}
