using InvoiceManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using InvoiceManagement.Application.DTOs;
using InvoiceManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceManagement.Web.Controllers
{
    [Authorize(Policy = "ApiPolicy")]
    public class InvoiceController : Controller
    {
        private readonly ApiService _apiService;

        public InvoiceController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var invoices = await _apiService.GetInvoicesAsync();
            return View(invoices);
        }

        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _apiService.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        public async Task<IActionResult> Create()
        {
            var model = new InvoiceViewModel
            {
                Items = new List<InvoiceItemViewModel>() // Initialize the list here
            };
            ViewBag.Stores = await _apiService.GetStoresAsync();
            ViewBag.Items = await _apiService.GetItemsAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceViewModel invoice)
        {
            if (ModelState.IsValid)
            {
                await _apiService.CreateInvoiceAsync(invoice);
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await _apiService.DeleteInvoiceAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteInvoiceAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
