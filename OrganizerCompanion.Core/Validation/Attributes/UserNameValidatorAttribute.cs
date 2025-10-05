using System.ComponentModel.DataAnnotations;

namespace OrganizerCompanion.Core.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    internal class UserNameValidatorAttribute : RegularExpressionAttribute
    {
        internal UserNameValidatorAttribute() : base(RegexValidators.UserNameRegexPattern)
        {
            ErrorMessage = "The username is not in a valid format.";
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
