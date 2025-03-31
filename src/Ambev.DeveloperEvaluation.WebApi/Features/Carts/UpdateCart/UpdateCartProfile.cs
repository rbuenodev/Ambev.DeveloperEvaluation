﻿using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<UpdateCartRequest, UpdateCartCommand>();
            CreateMap<UpdateCartItemRequest, UpdateCartItemCommand>();
            CreateMap<UpdateCartResult, UpdateCartResponse>();
            CreateMap<UpdateCartItemResult, UpdateCartItemResponse>();
        }
    }
}
