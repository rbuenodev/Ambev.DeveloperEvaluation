using Ambev.DeveloperEvaluation.Application.CartItems.AddCartItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems.AddCartItem
{
    public class AddCartItemProfile : Profile
    {
        public AddCartItemProfile()
        {
            CreateMap<AddCartItemRequest, AddCartItemCommand>();
            CreateMap<AddCartItemResult, AddCartItemResponse>();
        }
    }
}
