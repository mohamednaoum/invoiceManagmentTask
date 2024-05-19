using InvoiceManagement.Application.DTOs;
using InvoiceManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
    {
        var items = await _itemService.GetAllItemsAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItem(int id)
    {
        var item = await _itemService.GetItemByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItem(ItemDto itemDto)
    {
        var item = await _itemService.CreateItemAsync(itemDto);
        return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(int id, ItemDto itemDto)
    {
        if (id != itemDto.Id)
        {
            return BadRequest();
        }
        var result = await _itemService.UpdateItemAsync(itemDto);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var result = await _itemService.DeleteItemAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}