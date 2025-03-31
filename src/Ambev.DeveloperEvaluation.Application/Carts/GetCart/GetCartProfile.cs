using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartProfile : Profile
    {
        public GetCartProfile()
        {
            CreateMap<CreateCartCommand, Cart>();
            CreateMap<Cart, GetCartResult>()
                .ForMember(c => c.TotalProducts, opt => opt.MapFrom(c => c.CalculateTotalDiscounts()))
                .ForMember(c => c.TotalDiscounts, opt => opt.MapFrom(c => c.CalculateTotalDiscounts()))
                .ForMember(c => c.Total, opt => opt.MapFrom(c => c.CalculateTotalProducts() - c.CalculateTotalDiscounts()));

            CreateMap<Product, GetCartProductResult>();
            CreateMap<CartItem, GetCartItemResult>()
                .ForMember(i => i.TotalDiscounts, opt => opt.MapFrom(i => i.CalculateDiscounts()))
                .ForMember(i => i.Total, opt => opt.MapFrom(i => i.CalculateTotal() - i.CalculateDiscounts()));

        }
    }
}
