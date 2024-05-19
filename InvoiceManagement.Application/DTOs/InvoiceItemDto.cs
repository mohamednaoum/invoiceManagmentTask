namespace InvoiceManagement.Application.DTOs;

public class InvoiceItemDto
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int UnitId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}