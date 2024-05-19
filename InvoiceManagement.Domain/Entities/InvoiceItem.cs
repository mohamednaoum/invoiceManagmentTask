using InvoiceManagement.Domain.ValueObjects;

namespace InvoiceManagement.Domain.Entities;

public class InvoiceItem : Entity
{
    public int ItemId { get; private set; }
    public int UnitId { get; private set; }
    public Money Price { get; private set; }
    public int Quantity { get; private set; }
    public Money Total { get; private set; }
    public int InvoiceId { get; set; }


    // Constructor
    private InvoiceItem() { }

    public InvoiceItem(int itemId, int unitId, Money price, int quantity)
    {
        ItemId = itemId;
        UnitId = unitId;
        Price = price;
        Quantity = quantity;
        Total = price * quantity;
    }
}
