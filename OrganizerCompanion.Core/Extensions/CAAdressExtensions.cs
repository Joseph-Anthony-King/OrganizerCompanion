using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Extensions
{
    internal static class CAAdressExtensions
    {
        /// <summary>
        /// Casts a Domain ICAAddress to a Type ICAAddress
        /// </summary>
        public static Interfaces.Type.ICAAddress AsTypeCAAddress(this ICAAddress domainCAAddress)
        {
            return domainCAAddress; // Since Domain.ICAAddress inherits from Type.ICAAddress, this is a safe upcast
        }

        /// <summary>
        /// Casts a Type ICAAddress to a Domain ICAAddress (if possible)
        /// </summary>
        public static ICAAddress? AsDomainCAAddress(this Interfaces.Type.ICAAddress typeCAAddress)
        {
            return typeCAAddress as ICAAddress; // Returns null if the cast is not possible
        }

        /// <summary>
        /// Converts a list of Domain ICAAddresses to Type ICAAddresses
        /// </summary>
        public static List<Interfaces.Type.ICAAddress?> AsTypeCAAddresses(this List<ICAAddress?> domainCAAddresses)
        {
            return [.. domainCAAddresses.Cast<Interfaces.Type.ICAAddress?>()];
        }

        /// <summary>
        /// Converts a list of Type ICAAddresses to Domain ICAAddresses where possible
        /// </summary>
        public static List<ICAAddress?> AsDomainCAAddresses(this List<Interfaces.Type.ICAAddress?> typeCAAddresses)
        {
            return [.. typeCAAddresses.OfType<ICAAddress>().Cast<ICAAddress?>()];
        }
    }
}
