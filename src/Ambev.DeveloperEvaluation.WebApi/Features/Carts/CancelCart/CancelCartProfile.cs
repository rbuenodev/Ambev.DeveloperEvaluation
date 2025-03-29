using Ambev.DeveloperEvaluation.Application.Carts.ChangeStatusCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CancelCart
{
    public class CancelCartProfile : Profile
    {
        public CancelCartProfile()
        {
            CreateMap<CancelCartRequest, ChangeStatusCartCommand>();
            CreateMap<ChangeStatusCartResult, CancelCartResponse>();
        }
    }
}
