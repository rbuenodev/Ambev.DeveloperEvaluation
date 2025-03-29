﻿using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartItems.RemoveCartItem
{
    public class RemoveCartItemCommand : IRequest<RemoveCartItemResult>
    {
        public Guid Id { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new RemoveCartItemValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
