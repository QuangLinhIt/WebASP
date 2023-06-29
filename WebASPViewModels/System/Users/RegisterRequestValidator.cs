﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebASP.ViewModels.System.Users
{
    public class RegisterRequestValidator: AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required").MaximumLength(200).WithMessage("First Name can not over 200  characters "); ;
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required").MaximumLength(200).WithMessage("Last Name can not over 200  characters "); ;
            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birthday cannot greater than 100 years");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email format not match");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phonenumber is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Password is required").MinimumLength(6).WithMessage("password is at lease 6 characters");
            RuleFor(x => x).Custom((request, context) =>
              {
                  if(request.Password!=request.ConfirmPassword)
                  {
                      context.AddFailure("Confirm password is not match");
                  }
              });
        }
    }
}
