using InvoiceManagement.Application.DTOs;
using InvoiceManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class InvoiceApiController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceApiController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetInvoices()
    {
        var invoices = await _invoiceService.GetAllAsync();
        return Ok(invoices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceDto>> GetInvoice(int id)
    {
        var invoice = await _invoiceService.GetByIdAsync(id);
        if (invoice == null)
        {
            return NotFound();
        }
        return Ok(invoice);
    }

    [HttpPost]
    public async Task<ActionResult> CreateInvoice(InvoiceDto invoiceDto)
    {
        await _invoiceService.AddAsync(invoiceDto);
        return CreatedAtAction(nameof(GetInvoice), new { id = invoiceDto.Id }, invoiceDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteInvoice(int id)
    {
        await _invoiceService.DeleteAsync(id);
        return NoContent();
    }
}
