namespace InvoiceManagement.Application.DTOs;

public class InvoiceDto
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime Date { get; set; }
    public int StoreId { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public decimal Net { get; set; }
    public List<InvoiceItemDto> Items { get; set; }
}