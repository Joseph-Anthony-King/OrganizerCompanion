using System.ComponentModel.DataAnnotations;

namespace OrganizerCompanion.Core.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    internal class UrlValidatorAttribute : RegularExpressionAttribute
    {
        internal UrlValidatorAttribute() : base(RegexValidators.UrlRegexPattern)
        {
            ErrorMessage = "The URL is not in a valid format.";
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
