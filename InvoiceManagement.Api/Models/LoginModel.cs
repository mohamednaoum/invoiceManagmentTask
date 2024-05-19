using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.Api.Models;

public class LoginModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
}