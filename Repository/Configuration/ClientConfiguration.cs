using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entities;

namespace Repository.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Surname).IsRequired().HasMaxLength(50);
            builder.Property(p => p.MiddleName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.BurthDate).IsRequired();
            builder.Property(p => p.Address).IsRequired().HasMaxLength(200);
            builder.Property(p => p.PhoneNumber).IsRequired(false);
            builder.Property(p => p.SexId).IsRequired();
            builder.HasMany(p => p.Visits).WithOne(o => o.Client).HasForeignKey(pe => pe.ClientId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
