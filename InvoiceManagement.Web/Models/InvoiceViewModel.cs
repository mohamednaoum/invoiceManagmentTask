using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.Web.Models;
public class InvoiceViewModel
{
    public string InvoiceNumber { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    public int StoreId { get; set; }

    public List<InvoiceItemViewModel> Items { get; set; } = new List<InvoiceItemViewModel>();

    public decimal Total => CalculateTotal();
    public decimal Discount { get; set; }
    public decimal Net => Total - Discount;

    private decimal CalculateTotal()
    {
        decimal total = 0;
        foreach (var item in Items)
        {
            total += item.Price * item.Quantity;
        }
        return total;
    }
}

public class InvoiceItemViewModel
{
    public int ItemId { get; set; }
    public string Unit { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total => Price * Quantity;
}
