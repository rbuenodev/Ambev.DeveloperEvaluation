using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct
{
    /// <summary>
    /// Profile for mapping between Application and API CreateProduct responses
    /// </summary>
    public class DeleteProductProfile : Profile
    {    /// <summary>
         /// Initializes the mappings for CreateProduct feature
         /// </summary>
        public DeleteProductProfile()
        {
            CreateMap<DeleteProductRequest, DeleteProductCommand>();
            CreateMap<DeleteProductResult, DeleteProductResponse>();
        }
    }
}
