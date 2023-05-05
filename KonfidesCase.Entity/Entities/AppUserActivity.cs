namespace KonfidesCase.Entity.Entities
{
    public class AppUserActivity
    {
        #region Properties
        public Guid UserId { get; set; }
        public Guid ActivityId{ get; set; }
        #endregion

        #region Navigation Properties
        public AppUser User { get; set; } = new AppUser();
        public Activity Activity { get; set; } = new Activity();
        #endregion
    }
}
