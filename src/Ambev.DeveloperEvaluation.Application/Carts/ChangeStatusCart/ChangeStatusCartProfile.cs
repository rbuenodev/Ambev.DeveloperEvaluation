using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.ChangeStatusCart
{
    public class ChangeStatusCartProfile : Profile
    {
        public ChangeStatusCartProfile()
        {
            CreateMap<ChangeStatusCartCommand, Cart>();
        }
    }
}
