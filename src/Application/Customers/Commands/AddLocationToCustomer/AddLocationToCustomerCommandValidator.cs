﻿using FluentValidation;

namespace FreightManagement.Application.Customers.Commands.AddLocationToCustomer
{
    public class AddLocationToCustomerCommandValidator : AbstractValidator<AddLocationToCustomerCommand>
    {
        public AddLocationToCustomerCommandValidator()
        {
            RuleFor(v => v).NotNull().WithMessage("Location is required.");

            RuleFor(v => v.Id)
                .NotNull().WithMessage("Customer Id is required.")
                .Equal(0).WithMessage("Customer Id is required.");

            RuleFor(v => v.LocationId)
                .NotNull().WithMessage("Location Id is required.")
                .Equal(0).WithMessage("Location Id is required.");

        }
    }
}
