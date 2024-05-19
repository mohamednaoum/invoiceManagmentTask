using AutoMapper;
using InvoiceManagement.Application.DTOs;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Domain.ValueObjects;

namespace InvoiceManagement.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Store, StoreDto>();
        CreateMap<Item, ItemDto>();
        CreateMap<Invoice, InvoiceDto>().ReverseMap();
        CreateMap<InvoiceItem, InvoiceItemDto>().ReverseMap();
        CreateMap<Money, decimal>().ConvertUsing(m => m.Amount);
        CreateMap<decimal, Money>().ConvertUsing(a => new Money(a));
    }
}
