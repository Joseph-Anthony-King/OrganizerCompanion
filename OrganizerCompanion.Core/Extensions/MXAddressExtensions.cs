using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Extensions
{
    internal static class MXAddressExtensions
    {
        /// <summary>
        /// Casts a Domain IMXAddress to a Type IMXAddress
        /// </summary>
        public static Interfaces.Type.IMXAddress AsTypeMXAddress(this IMXAddress domainMXAddress)
        {
            return (Interfaces.Type.IMXAddress)domainMXAddress; // Explicit cast since Domain.IMXAddress inherits from Type.IMXAddress
        }

        /// <summary>
        /// Casts a Type IMXAddress to a Domain IMXAddress (if possible)
        /// </summary>
        public static IMXAddress? AsDomainMXAddress(this Interfaces.Type.IMXAddress typeMXAddress)
        {
            return typeMXAddress as IMXAddress; // Returns null if the cast is not possible
        }

        /// <summary>
        /// Converts a list of Domain IMXAddresses to Type IMXAddresses
        /// </summary>
        public static List<Interfaces.Type.IMXAddress?> AsTypeMXAddresses(this List<IMXAddress?> domainMXAddresses)
        {
            return [.. domainMXAddresses.Cast<Interfaces.Type.IMXAddress?>()];
        }

        /// <summary>
        /// Converts a list of Type IMXAddresses to Domain IMXAddresses where possible
        /// </summary>
        public static List<IMXAddress?> AsDomainMXAddresses(this List<Interfaces.Type.IMXAddress?> typeMXAddresses)
        {
            return [.. typeMXAddresses.OfType<IMXAddress>().Cast<IMXAddress?>()];
        }
    }
}
