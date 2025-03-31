using Ambev.DeveloperEvaluation.Application.Carts.ChangeStatusCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CloseCart
{
    public class CloseCartProfile : Profile
    {
        public CloseCartProfile()
        {
            CreateMap<CloseCartRequest, ChangeStatusCartCommand>();
            CreateMap<ChangeStatusCartResult, CloseCartResponse>();
        }
    }
}
