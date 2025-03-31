using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.CartItems.AddCartItem
{
    public class AddCartItemProfile : Profile
    {
        public AddCartItemProfile()
        {
            CreateMap<AddCartItemCommand, CartItem>();
        }
    }
}
