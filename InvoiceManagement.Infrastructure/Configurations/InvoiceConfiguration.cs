using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Infrastructure.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.Infrastructure.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(50);
        builder.Property(i => i.Total).HasConversion<MoneyConverter>().IsRequired();
        builder.Property(i => i.Discount).HasConversion<MoneyConverter>().IsRequired();
        builder.Property(i => i.Net).HasConversion<MoneyConverter>().IsRequired();
        builder.HasMany(i => i.Items).WithOne().HasForeignKey(ii => ii.InvoiceId);
    }
}