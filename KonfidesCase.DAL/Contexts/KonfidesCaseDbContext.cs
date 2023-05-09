using KonfidesCase.Entity.Entities;
using KonfidesCase.Entity.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace KonfidesCase.DAL.Contexts
{
    public class KonfidesCaseDbContext : DbContext
    {
        #region Field & Constructor
        internal const string ConnectionName = "Default";
        public KonfidesCaseDbContext(DbContextOptions<KonfidesCaseDbContext> options) : base(options)
        {
            
        }
        #endregion

        #region OnModelCreating Method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActivityConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserActivityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region Properties
        public DbSet<Activity> Activities { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppUserActivity> UserActivity { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        #endregion
    }
}
