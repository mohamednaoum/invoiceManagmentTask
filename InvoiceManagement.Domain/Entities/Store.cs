namespace InvoiceManagement.Domain.Entities;

public class Store:Entity
{
    public string Name { get; set; }

    public Store(int id , string name)
    {
        Name = name;
        Id = id;
    }
}