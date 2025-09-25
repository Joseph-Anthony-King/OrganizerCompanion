using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Extensions
{
    internal static class USAddressExtensions
    {
        /// <summary>
        /// Casts a Domain IUSAddress to a Type IUSAddress
        /// </summary>
        public static Interfaces.Type.IUSAddress AsTypeUSAddress(this IUSAddress domainUSAddress)
        {
            return domainUSAddress; // Since Domain.IUSAddress inherits from Type.IUSAddress, this is a safe upcast
        }

        /// <summary>
        /// Casts a Type IUSAddress to a Domain IUSAddress (if possible)
        /// </summary>
        public static IUSAddress? AsDomainUSAddress(this Interfaces.Type.IUSAddress typeUSAddress)
        {
            return typeUSAddress as IUSAddress; // Returns null if the cast is not possible
        }

        /// <summary>
        /// Converts a list of Domain IUSAddresses to Type IUSAddresses
        /// </summary>
        public static List<Interfaces.Type.IUSAddress?> AsTypeUSAddresses(this List<IUSAddress?> domainUSAddresses)
        {
            return [.. domainUSAddresses.Cast<Interfaces.Type.IUSAddress?>()];
        }

        /// <summary>
        /// Converts a list of Type IUSAddresses to Domain IUSAddresses where possible
        /// </summary>
        public static List<IUSAddress?> AsDomainUSAddresses(this List<Interfaces.Type.IUSAddress?> typeUSAddresses)
        {
            return [.. typeUSAddresses.OfType<IUSAddress>().Cast<IUSAddress?>()];
        }
    }
}