using Microsoft.EntityFrameworkCore;
using User_Service.API.Models;

namespace User_Service.API.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Phone).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(c => c.FullName).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(c => c.Password).HasColumnType("VARCHAR(256)").IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.OwnsOne(c => c.Email).Property(c => c.Address).HasColumnType("VARCHAR(150)").HasColumnName("Email").IsRequired();
            builder.OwnsOne(c => c.Document).Property(c => c.Number).HasColumnType("VARCHAR(150)").HasColumnName("Document").IsRequired();
        }
    }
}
