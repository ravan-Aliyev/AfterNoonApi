using AfterNoonV2.Application.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Please enter name")
                .MaximumLength(125)
                .MinimumLength(2)
                    .WithMessage("Please enter 2 or 125 character");

            RuleFor(x => x.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Please enter stock")
                .Must(s => s >= 0)
                    .WithMessage("Stock can not be a negative number");

            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Please enter price")
                .Must(s => s >= 0)
                    .WithMessage("Price can not be a negative");
        }
    }
}
