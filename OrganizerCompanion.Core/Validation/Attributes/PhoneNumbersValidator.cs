using System.ComponentModel.DataAnnotations;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
    internal class PhoneNumbersValidator : RegularExpressionAttribute
    {
        internal PhoneNumbersValidator() : base(RegexValidators.NANPPhoneNumberRegexPattern)
        {
            ErrorMessage = "One or more phone numbers are not in a valid format.";
        }
        public override bool IsValid(object? value)
        {
            // Handle IEnumerable<string> (most common case for lists of phone numbers)
            if (value is IEnumerable<string> phoneStrings)
            {
                foreach (var phoneNumber in phoneStrings)
                {
                    // An empty or null string in the list is considered invalid.
                    if (string.IsNullOrEmpty(phoneNumber))
                    {
                        return false;
                    }
                    // Use the base validation for each phone number. If one is invalid, the whole list is invalid.
                    if (!base.IsValid(phoneNumber))
                    {
                        return false;
                    }
                }
                // If all phone numbers in the list are valid, return true.
                return true;
            }
            // Handle IEnumerable<PhoneNumberDTO>
            else if (value is IEnumerable<PhoneNumberDTO> phoneNumberDTOs)
            {
                foreach (var phoneNumberDTO in phoneNumberDTOs)
                {
                    // An empty or null phone string is considered invalid.
                    if (string.IsNullOrEmpty(phoneNumberDTO.Phone))
                    {
                        return false;
                    }
                    // Use the base validation for each phone number. If one is invalid, the whole list is invalid.
                    if (!base.IsValid(phoneNumberDTO.Phone))
                    {
                        return false;
                    }
                }
                // If all phone numbers in the list are valid, return true.
                return true;
            }
            // Handle IEnumerable<PhoneNumber> (domain objects)
            else if (value is IEnumerable<PhoneNumber> phoneNumbers)
            {
                foreach (var phoneNumber in phoneNumbers)
                {
                    // An empty or null phone string is considered invalid.
                    if (string.IsNullOrEmpty(phoneNumber.Phone))
                    {
                        return false;
                    }
                    // Use the base validation for each phone number. If one is invalid, the whole list is invalid.
                    if (!base.IsValid(phoneNumber.Phone))
                    {
                        return false;
                    }
                }
                // If all phone numbers in the list are valid, return true.
                return true;
            }
            // For all other types, fall back to base validation
            return base.IsValid(value);
        }
    }
}
