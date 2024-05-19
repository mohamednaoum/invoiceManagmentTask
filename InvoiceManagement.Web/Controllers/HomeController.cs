using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.Web.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}