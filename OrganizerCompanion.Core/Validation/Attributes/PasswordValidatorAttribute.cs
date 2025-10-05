using System.ComponentModel.DataAnnotations;

namespace OrganizerCompanion.Core.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    internal class PasswordValidatorAttribute : RegularExpressionAttribute
    {
        internal PasswordValidatorAttribute() : base(RegexValidators.PasswordRegexPattern)
        {
            ErrorMessage = "The password must be from 4 and up through 20 characters with at least 1 upper case letter, 1 lower case letter, 1 numeric character, and 1 special character of ! @ # $ % ^ & * + = ? - _ . ,";
        }

        public override bool IsValid(object? value)
        {
            if (value is string s && string.IsNullOrEmpty(s))
            {
                return false;
            }

            return base.IsValid(value);
        }
    }
}
