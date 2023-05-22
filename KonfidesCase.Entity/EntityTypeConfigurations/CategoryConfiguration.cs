namespace KonfidesCase.Entity.EntityTypeConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        #region Fluent API
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Kategoriler");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired()
                .HasColumnOrder(1);
            builder.Property(c => c.Name).IsRequired()
                .HasColumnOrder (2)
                .HasColumnName("Ad");            
        }
        #endregion
    }
}
