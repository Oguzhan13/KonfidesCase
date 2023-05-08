namespace KonfidesCase.Entity.Entities
{
    public class AppUserActivity
    {
        #region Constructor
        public AppUserActivity()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ActivityId{ get; set; }
        #endregion

        #region Navigation Properties
        public AppUser? User { get; set; }
        public Activity? Activity { get; set; }
        #endregion
    }
}
