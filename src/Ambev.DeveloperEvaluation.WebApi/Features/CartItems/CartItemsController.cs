using Ambev.DeveloperEvaluation.Application.CartItems.AddCartItem;
using Ambev.DeveloperEvaluation.Application.CartItems.DecreaseCartItem;
using Ambev.DeveloperEvaluation.Application.CartItems.RemoveCartItem;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.CartItems.AddCartItem;
using Ambev.DeveloperEvaluation.WebApi.Features.CartItems.DecreaseCartItem;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartItemsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveCartItem([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new RemoveCartItemCommand { Id = id };
            var validator = new RemoveCartItemValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResponse
            {
                Success = response.Success,
                Message = response.Success ? "Cart item removed successfully" : "Cart item not found"
            });
        }

        [HttpPost("add")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartItemRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AddCartItemCommand>(request);
            var validator = new AddCartItemValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResponse
            {
                Success = response.Success,
                Message = response.Success ? "Cart item added successfully" : "Cart item not found",
            });
        }

        [HttpPost("decrease")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DecreaseCartItem([FromBody] DecreaseCartItemRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DecreaseCartItemCommand>(request);
            var validator = new DecreaseCartItemValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResponse
            {
                Success = response.Success,
                Message = response.Success ? "Cart item decreased successfully" : "Cart item not found",
            });
        }
    }
}
