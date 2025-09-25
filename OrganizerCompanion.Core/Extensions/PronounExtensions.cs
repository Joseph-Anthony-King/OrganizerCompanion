using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Extensions
{
    internal static class PronounExtensions
    {
        private static readonly Dictionary<Pronouns, (string Subject, string Object, string Possessive, string Display)> PronounData = new()
        {
            // Traditional pronouns
            { Pronouns.SheHer, ("she", "her", "hers", "she/her") },
            { Pronouns.HeHim, ("he", "him", "his", "he/him") },
            
            // Gender-neutral pronouns
            { Pronouns.TheyThem, ("they", "them", "theirs", "they/them") },
            
            // Neo-pronouns
            { Pronouns.XeXir, ("xe", "xir", "xirs", "xe/xir") },
            { Pronouns.ZeZir, ("ze", "zir", "zirs", "ze/zir") },
            { Pronouns.EyEm, ("ey", "em", "eirs", "ey/em") },
            { Pronouns.FaeFaer, ("fae", "faer", "faers", "fae/faer") },
            { Pronouns.VeVer, ("ve", "ver", "vers", "ve/ver") },
            { Pronouns.PerPer, ("per", "per", "pers", "per/per") },
            
            // Other options
            { Pronouns.PreferNotToSay, ("", "", "", "Prefer not to say") },
            { Pronouns.Other, ("", "", "", "Other") }
        };

        /// <summary>
        /// Gets the subject pronoun (e.g., "he", "she", "they")
        /// </summary>
        public static string GetSubject(this Pronouns pronoun)
        {
            return PronounData.TryGetValue(pronoun, out var data) ? data.Subject : pronoun.ToString();
        }

        /// <summary>
        /// Gets the object pronoun (e.g., "him", "her", "them")
        /// </summary>
        public static string GetObject(this Pronouns pronoun)
        {
            return PronounData.TryGetValue(pronoun, out var data) ? data.Object : pronoun.ToString();
        }

        /// <summary>
        /// Gets the possessive pronoun (e.g., "his", "hers", "theirs")
        /// </summary>
        public static string GetPossessive(this Pronouns pronoun)
        {
            return PronounData.TryGetValue(pronoun, out var data) ? data.Possessive : pronoun.ToString();
        }

        /// <summary>
        /// Gets the display format for the pronouns (e.g., "he/him", "she/her", "they/them")
        /// </summary>
        public static string GetDisplayFormat(this Pronouns pronoun)
        {
            return PronounData.TryGetValue(pronoun, out var data) ? data.Display : pronoun.ToString();
        }

        /// <summary>
        /// Formats a sentence using the subject pronoun with proper capitalization
        /// </summary>
        /// <param name="pronoun">The pronoun enum value</param>
        /// <param name="capitalize">Whether to capitalize the first letter (default: true)</param>
        /// <returns>The formatted subject pronoun</returns>
        public static string GetFormattedSubject(this Pronouns pronoun, bool capitalize = true)
        {
            var subject = pronoun.GetSubject();
            if (string.IsNullOrEmpty(subject)) return string.Empty;
            
            return capitalize ? char.ToUpper(subject[0]) + subject.Substring(1) : subject;
        }
    }
}