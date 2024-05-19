using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Infrastructure.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.Infrastructure.Configurations;

public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.HasKey(ii => ii.Id);
        builder.Property(ii => ii.Price).HasConversion<MoneyConverter>().IsRequired();
        builder.Property(ii => ii.Total).HasConversion<MoneyConverter>().IsRequired();
        builder.HasOne<Invoice>()
            .WithMany(i => i.Items)
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
