using InvoiceManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvoiceManagement.Infrastructure.Helper;

public class MoneyConverter : ValueConverter<Money, decimal>
{
    public MoneyConverter() : base(
        v => v.Amount,
        v => new Money(v))
    {
    }
}