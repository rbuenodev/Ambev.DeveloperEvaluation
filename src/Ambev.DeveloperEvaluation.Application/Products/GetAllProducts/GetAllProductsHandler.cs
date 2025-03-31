using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, ICollection<GetAllProductsResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ICollection<GetAllProductsResult>> Handle(GetAllProductsCommand command, CancellationToken cancellationToken)
        {
            var validator = new GetAllProductsValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var produts = await _productRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<ICollection<GetAllProductsResult>>(produts);
        }
    }
}
