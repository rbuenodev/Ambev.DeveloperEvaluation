using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<UpdateCartCommand, Cart>();
            CreateMap<UpdateCartItemCommand, CartItem>();

            CreateMap<Cart, UpdateCartResult>()
                .ForMember(c => c.TotalProducts, opt => opt.MapFrom(c => c.CalculateTotalDiscounts()))
                .ForMember(c => c.TotalDiscounts, opt => opt.MapFrom(c => c.CalculateTotalDiscounts()))
                .ForMember(c => c.Total, opt => opt.MapFrom(c => c.CalculateTotalProducts() - c.CalculateTotalDiscounts()));

            CreateMap<CartItem, UpdateCartItemResult>()
                 .ForMember(i => i.TotalDiscounts, opt => opt.MapFrom(i => i.CalculateDiscounts()))
                .ForMember(i => i.Total, opt => opt.MapFrom(i => i.CalculateTotal() - i.CalculateDiscounts()));
        }
    }
}
