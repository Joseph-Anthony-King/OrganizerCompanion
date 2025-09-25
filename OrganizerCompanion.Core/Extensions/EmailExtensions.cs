using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.Extensions
{
    internal static class EmailExtensions
    {
        /// <summary>
        /// Casts a Domain IEmail to a Type IEmail
        /// </summary>
        public static Interfaces.Type.IEmail AsTypeEmail(this IEmail domainEmail)
        {
            return domainEmail; // Since Domain.IEmail inherits from Type.IEmail, this is a safe upcast
        }

        /// <summary>
        /// Casts a Type IEmail to a Domain IEmail (if possible)
        /// </summary>
        public static IEmail? AsDomainEmail(this Interfaces.Type.IEmail typeEmail)
        {
            return typeEmail as IEmail; // Returns null if the cast is not possible
        }

        /// <summary>
        /// Creates a new Domain Email from a Type IEmail
        /// </summary>
        public static Email ToDomainEmail(this Interfaces.Type.IEmail typeEmail)
        {
            return new Email
            {
                EmailAddress = typeEmail.EmailAddress
            };
        }

        /// <summary>
        /// Converts a list of Domain IEmails to Type IEmails
        /// </summary>
        public static List<Interfaces.Type.IEmail?> AsTypeEmails(this List<IEmail?> domainEmails)
        {
            return [.. domainEmails.Cast<Interfaces.Type.IEmail?>()];
        }

        /// <summary>
        /// Converts a list of Type IEmails to Domain IEmails where possible
        /// </summary>
        public static List<IEmail?> AsDomainEmails(this List<Interfaces.Type.IEmail?> typeEmails)
        {
            return [.. typeEmails.OfType<IEmail>().Cast<IEmail?>()];
        }
    }
}