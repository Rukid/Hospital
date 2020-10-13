using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entities;

namespace Repository.Configuration
{
    public class SexConfiguration : IEntityTypeConfiguration<Sex>
    {
        public void Configure(EntityTypeBuilder<Sex> builder)
        {
            builder.ToTable("Sex");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
            builder.HasMany(p => p.Clients).WithOne(o => o.Sex).HasForeignKey(pe => pe.SexId);
            builder.HasMany(p => p.Doctors).WithOne(o => o.Sex).HasForeignKey(pe => pe.SexId);
        }
    }
}
