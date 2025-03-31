using Ambev.DeveloperEvaluation.Application.CartItems.RemoveCartItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems.RemoveCartItem
{
    public class RemoveCartItemProfile : Profile
    {
        public RemoveCartItemProfile()
        {
            CreateMap<RemoveCartItemRequest, RemoveCartItemCommand>();
            CreateMap<RemoveCartItemResult, RemoveCartItemResponse>();
        }
    }
}
