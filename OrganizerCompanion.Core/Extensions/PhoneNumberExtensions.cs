using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Extensions
{
    internal static class PhoneNumberExtensions
    {
        /// <summary>
        /// Casts a Domain IPhoneNumber to a Type IPhoneNumber
        /// </summary>
        public static Interfaces.Type.IPhoneNumber AsTypePhoneNumber(this IPhoneNumber domainPhoneNumber)
        {
            return domainPhoneNumber; // Since Domain.IPhoneNumber inherits from Type.IPhoneNumber, this is a safe upcast
        }

        /// <summary>
        /// Casts a Type IPhoneNumber to a Domain IPhoneNumber (if possible)
        /// </summary>
        public static IPhoneNumber? AsDomainPhoneNumber(this Interfaces.Type.IPhoneNumber typePhoneNumber)
        {
            return typePhoneNumber as IPhoneNumber; // Returns null if the cast is not possible
        }

        /// <summary>
        /// Converts a list of Domain IPhoneNumbers to Type IPhoneNumbers
        /// </summary>
        public static List<Interfaces.Type.IPhoneNumber?> AsTypePhoneNumbers(this List<IPhoneNumber?> domainPhoneNumbers)
        {
            return [.. domainPhoneNumbers.Cast<Interfaces.Type.IPhoneNumber?>()];
        }

        /// <summary>
        /// Converts a list of Type IPhoneNumbers to Domain IPhoneNumbers where possible
        /// </summary>
        public static List<IPhoneNumber?> AsDomainPhoneNumbers(this List<Interfaces.Type.IPhoneNumber?> typePhoneNumbers)
        {
            return [.. typePhoneNumbers.OfType<IPhoneNumber>().Cast<IPhoneNumber?>()];
        }
    }
}