using FluentValidation;
using ShopBridge.Api.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Api.Application.Helpers
{
    public class ProductValidator : AbstractValidator<Products>
    {
        /// <summary>  
        /// Validator rules for Product  
        /// </summary>  
        public ProductValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("The Product ID must be at greather than 0.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("The Product Name cannot be blank.")
                .Length(0, 100)
                .WithMessage("The Product Name cannot be more than 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("The Product Description cannot be blank");

            RuleFor(x => x.Rate).GreaterThan(0).WithMessage("The Price must be at greather than 0.");

        }
    }
}
