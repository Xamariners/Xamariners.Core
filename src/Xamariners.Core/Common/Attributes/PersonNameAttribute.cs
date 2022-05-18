using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Xamariners.Core.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class PersonNameAttribute : ValidationAttribute
    {
        private const string PERSON_NAME_REGEX_PATTERN = "^[\\p{L}'][ \\p{L}\\p{IsKatakana}\\p{IsHiragana}'-]*[\\p{L}]$";

        /// <summary>
        /// Gets or sets the custom required name error message.
        /// </summary>
        public string RequiredNameErrorMessage { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string personName = value as string;

            if (string.IsNullOrEmpty(personName))
            {
                if (string.IsNullOrEmpty(RequiredNameErrorMessage))
                    return new ValidationResult("Person name is required.");
                else
                    return new ValidationResult(RequiredNameErrorMessage);
            }

            var isMatch = Regex.Match(personName.Trim(), PERSON_NAME_REGEX_PATTERN);

            if (!isMatch.Success)
            {
                if (string.IsNullOrEmpty(ErrorMessage))
                    return new ValidationResult("Person name is required and must be alphabetical characters only.");
                else
                    return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        public override bool IsValid(object value)
        {
            var validationResult = IsValid(value, null);
            return validationResult is null;
        }
    }
}

