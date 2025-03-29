using Ambev.DeveloperEvaluation.Application.CartItems.DecreaseCartItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems.DecreaseCartItem
{
    public class DecreaseCartItemProfile : Profile
    {
        public DecreaseCartItemProfile()
        {
            CreateMap<DecreaseCartItemRequest, DecreaseCartItemCommand>();
            CreateMap<DecreaseCartItemResult, DecreaseCartItemResponse>();
        }
    }
}
