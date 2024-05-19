using InvoiceManagement.Domain.ValueObjects;

namespace InvoiceManagement.Domain.Entities;

public class Invoice : Entity
{
    public string InvoiceNumber { get; private set; }
    public DateTime Date { get; private set; }
    public int StoreId { get; private set; }
    public Money Total { get; private set; }
    public Money Discount { get; private set; }
    public Money Net { get; private set; }
    public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();
    private List<InvoiceItem> _items;

    // Constructor
    private Invoice() { }

    public Invoice(string invoiceNumber, DateTime date, int storeId, Money total, Money discount, Money net)
    {
        InvoiceNumber = invoiceNumber;
        Date = date;
        StoreId = storeId;
        Total = total;
        Discount = discount;
        Net = net;
        _items = new List<InvoiceItem>();
    }

    // Methods to manage the Invoice items
    public void AddItem(int itemId, int unitId, Money price, int quantity)
    {
        var newItem = new InvoiceItem(itemId, unitId, price, quantity);
        _items.Add(newItem);
    }
}
