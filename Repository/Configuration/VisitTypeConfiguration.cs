using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entities;

namespace Repository.Configuration
{
    public class VisitTypeConfiguration : IEntityTypeConfiguration<VisitType>
    {
        public void Configure(EntityTypeBuilder<VisitType> builder)
        {
            builder.ToTable("VisitTypes");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.VisitName).IsRequired().HasMaxLength(20);
            builder.HasMany(p => p.Visits).WithOne(o => o.VisitType).HasForeignKey(pe => pe.VisitId);
        }
    }
}
