using System.ComponentModel.DataAnnotations;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    internal class EmailsValidatorAttribute : RegularExpressionAttribute
    {
        internal EmailsValidatorAttribute() : base(RegexValidators.EmailRegexPattern)
        {
            ErrorMessage = "One or more email addresses are not in a valid format.";
        }

        public override bool IsValid(object? value)
        {
            // Handle IEnumerable<string> first (most common case for lists of phone numbers)
            if (value is IEnumerable<string> emailStrings)
            {
                foreach (var email in emailStrings)
                {
                    // An empty or null string in the list is considered invalid.
                    if (string.IsNullOrEmpty(email))
                    {
                        return false;
                    }
                    // Use the base validation for each email. If one is invalid, the whole list is invalid.
                    if (!base.IsValid(email))
                    {
                        return false;
                    }
                }
                // If all emails in the list are valid, return true.
                return true;
            }
            else if (value is IEnumerable<EmailDTO> emailDTOs)
            {
                foreach (var email in emailDTOs)
                {
                    // An empty or null string in the list is considered invalid.
                    if (string.IsNullOrEmpty(email.EmailAddress))
                    {
                        return false;
                    }
                    // Use the base validation for each email. If one is invalid, the whole list is invalid.
                    if (!base.IsValid(email.EmailAddress))
                    {
                        return false;
                    }
                }
                // If all emails in the list are valid, return true.
                return true;
            }
            // If the value is a list of EmailDTO, validate each one.
            else if (value is IEnumerable<Email> emails)
            {
                foreach (var email in emails)
                {
                    // An empty or null string in the list is considered invalid.
                    if (string.IsNullOrEmpty(email.EmailAddress))
                    {
                        return false;
                    }
                    // Use the base validation for each email. If one is invalid, the whole list is invalid.
                    if (!base.IsValid(email.EmailAddress))
                    {
                        return false;
                    }
                }
                // If all emails in the list are valid, return true.
                return true;
            }

            return base.IsValid(value);
        }
    }
}
