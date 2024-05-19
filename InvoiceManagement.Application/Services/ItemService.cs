using AutoMapper;
using InvoiceManagement.Application.DTOs;
using InvoiceManagement.Application.Interfaces;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Domain.Repositories;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public ItemService(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
    {
        var items = await _itemRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ItemDto>>(items);
    }

    public async Task<ItemDto> GetItemByIdAsync(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        return _mapper.Map<ItemDto>(item);
    }

    public async Task<ItemDto> CreateItemAsync(ItemDto itemDto)
    {
        var item = _mapper.Map<Item>(itemDto);
        await _itemRepository.AddAsync(item);
        return _mapper.Map<ItemDto>(item);
    }

    public async Task<bool> UpdateItemAsync(ItemDto itemDto)
    {
        var item = await _itemRepository.GetByIdAsync(itemDto.Id);
        if (item == null) return false;
        _mapper.Map(itemDto, item);
        await _itemRepository.UpdateAsync(item);
        return true;
    }

    public async Task<bool> DeleteItemAsync(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        if (item == null) return false;
        await _itemRepository.DeleteAsync(item);
        return true;
    }
}