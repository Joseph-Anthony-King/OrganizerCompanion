using System.ComponentModel.DataAnnotations;

namespace OrganizerCompanion.Core.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    internal class UrlValidatedAttribute : RegularExpressionAttribute
    {
        internal UrlValidatedAttribute() : base(RegexValidators.UrlRegexPattern)
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
