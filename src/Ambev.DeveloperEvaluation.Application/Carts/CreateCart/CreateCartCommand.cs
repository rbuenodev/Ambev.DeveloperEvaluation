using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartCommand : IRequest<CreateCartResult>
    {
        /// <summary>
        /// Gets the unique sale/order reference number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;
        /// <summary>
        /// Gets the branch associated with this cart.
        /// </summary>
        public Branch Branch { get; set; }
        /// <summary>
        /// Gets the userId associated with this cart.
        /// </summary>
        public Guid UserId { get; set; }


        public ValidationResultDetail Validate()
        {
            var validator = new CreateCartValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
