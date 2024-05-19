namespace InvoiceManagement.Domain.Entities;

public class Item
{
    public Item(int id, string itemName, decimal price)
    {
        Id = id;
        Name = itemName;
        Price = price;

    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}