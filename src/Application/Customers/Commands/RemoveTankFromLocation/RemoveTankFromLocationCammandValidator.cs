﻿using FluentValidation;

namespace FreightManagement.Application.Customers.Commands.RemoveTankFromLocation
{
    public class RemoveTankFromLocationCammandValidator : AbstractValidator<RemoveTankFromLocationCammand>
    {
        public RemoveTankFromLocationCammandValidator(){

            RuleFor(v => v).NotNull().WithMessage("Tank is required.");

            RuleFor(v => v.tankId)
                .NotNull().WithMessage("Tank Id is required.")
                .GreaterThan(0).WithMessage("Tank Id is required.");

            RuleFor(v => v.locationId)
                .NotNull().WithMessage("Location Id is required.")
                .GreaterThan(0).WithMessage("Location Id is required.");
        }

    }
}
