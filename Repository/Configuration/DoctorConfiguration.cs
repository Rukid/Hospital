using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entities;

namespace Repository.Configuration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Surname).IsRequired().HasMaxLength(50);
            builder.Property(p => p.MiddleName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Speciality).IsRequired().HasMaxLength(30);
            builder.HasMany(p => p.Visits).WithOne(o => o.Doctor).HasForeignKey(pe => pe.DoctorId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
