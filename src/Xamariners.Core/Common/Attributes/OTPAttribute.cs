using System;
using System.ComponentModel.DataAnnotations;
using Xamariners.Core.Common.Helpers;

namespace Xamariners.Core.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class OTPAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OTPAttribute"/> class.
        /// </summary>
        /// <param name="Length">The length.</param>
        public OTPAttribute(int length)
        {
            Length = length;
        }

        /// <summary>
        /// Gets the maximum length of the OTP.
        /// </summary>
        public int Length { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otpCode = value as string;
            if (string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = $"OTP should not be blank and must be a number of {Length} digits";

            if (string.IsNullOrEmpty(otpCode)   // Should not be blank
                || otpCode.Length < 4           // Should not less then 4 digits
                || otpCode.Length > Length      // Should not more than specified "Length" digits
                || !otpCode.IsNumber())         // Should only be a number characters
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
