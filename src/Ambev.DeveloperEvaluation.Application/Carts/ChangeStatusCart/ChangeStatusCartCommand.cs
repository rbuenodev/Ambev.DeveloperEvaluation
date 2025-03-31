using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ChangeStatusCart
{
    public class ChangeStatusCartCommand : IRequest<ChangeStatusCartResult>
    {
        public Guid Id { get; set; }
        public CartStatus Status { get; set; }
        public ValidationResultDetail Validate()
        {
            var validator = new ChangeStatusCartValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
