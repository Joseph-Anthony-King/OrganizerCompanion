using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Extensions
{
    internal static class CountriesExtensions
    {
        private static readonly Dictionary<Countries, (string Name, string IsoCode)> CountryData = new()
        {
            // A
            { Countries.Afghanistan, ("Afghanistan", "AF") },
            { Countries.Albania, ("Albania", "AL") },
            { Countries.Algeria, ("Algeria", "DZ") },
            { Countries.Andorra, ("Andorra", "AD") },
            { Countries.Angola, ("Angola", "AO") },
            { Countries.AntiguaAndBarbuda, ("Antigua and Barbuda", "AG") },
            { Countries.Argentina, ("Argentina", "AR") },
            { Countries.Armenia, ("Armenia", "AM") },
            { Countries.Australia, ("Australia", "AU") },
            { Countries.Austria, ("Austria", "AT") },
            { Countries.Azerbaijan, ("Azerbaijan", "AZ") },
            
            // B
            { Countries.Bahamas, ("Bahamas", "BS") },
            { Countries.Bahrain, ("Bahrain", "BH") },
            { Countries.Bangladesh, ("Bangladesh", "BD") },
            { Countries.Barbados, ("Barbados", "BB") },
            { Countries.Belarus, ("Belarus", "BY") },
            { Countries.Belgium, ("Belgium", "BE") },
            { Countries.Belize, ("Belize", "BZ") },
            { Countries.Benin, ("Benin", "BJ") },
            { Countries.Bhutan, ("Bhutan", "BT") },
            { Countries.Bolivia, ("Bolivia", "BO") },
            { Countries.BosniaAndHerzegovina, ("Bosnia and Herzegovina", "BA") },
            { Countries.Botswana, ("Botswana", "BW") },
            { Countries.Brazil, ("Brazil", "BR") },
            { Countries.Brunei, ("Brunei", "BN") },
            { Countries.Bulgaria, ("Bulgaria", "BG") },
            { Countries.BurkinaFaso, ("Burkina Faso", "BF") },
            { Countries.Burundi, ("Burundi", "BI") },
            
            // C
            { Countries.CaboVerde, ("Cabo Verde", "CV") },
            { Countries.Cambodia, ("Cambodia", "KH") },
            { Countries.Cameroon, ("Cameroon", "CM") },
            { Countries.Canada, ("Canada", "CA") },
            { Countries.CentralAfricanRepublic, ("Central African Republic", "CF") },
            { Countries.Chad, ("Chad", "TD") },
            { Countries.Chile, ("Chile", "CL") },
            { Countries.China, ("China", "CN") },
            { Countries.Colombia, ("Colombia", "CO") },
            { Countries.Comoros, ("Comoros", "KM") },
            { Countries.Congo, ("Congo", "CG") },
            { Countries.CongoDemocraticRepublic, ("Democratic Republic of the Congo", "CD") },
            { Countries.CostaRica, ("Costa Rica", "CR") },
            { Countries.Croatia, ("Croatia", "HR") },
            { Countries.Cuba, ("Cuba", "CU") },
            { Countries.Cyprus, ("Cyprus", "CY") },
            { Countries.CzechRepublic, ("Czech Republic", "CZ") },
            
            // D
            { Countries.Denmark, ("Denmark", "DK") },
            { Countries.Djibouti, ("Djibouti", "DJ") },
            { Countries.Dominica, ("Dominica", "DM") },
            { Countries.DominicanRepublic, ("Dominican Republic", "DO") },
            
            // E
            { Countries.Ecuador, ("Ecuador", "EC") },
            { Countries.Egypt, ("Egypt", "EG") },
            { Countries.ElSalvador, ("El Salvador", "SV") },
            { Countries.EquatorialGuinea, ("Equatorial Guinea", "GQ") },
            { Countries.Eritrea, ("Eritrea", "ER") },
            { Countries.Estonia, ("Estonia", "EE") },
            { Countries.Eswatini, ("Eswatini", "SZ") },
            { Countries.Ethiopia, ("Ethiopia", "ET") },
            
            // F
            { Countries.Fiji, ("Fiji", "FJ") },
            { Countries.Finland, ("Finland", "FI") },
            { Countries.France, ("France", "FR") },
            
            // G
            { Countries.Gabon, ("Gabon", "GA") },
            { Countries.Gambia, ("Gambia", "GM") },
            { Countries.Georgia, ("Georgia", "GE") },
            { Countries.Germany, ("Germany", "DE") },
            { Countries.Ghana, ("Ghana", "GH") },
            { Countries.Greece, ("Greece", "GR") },
            { Countries.Grenada, ("Grenada", "GD") },
            { Countries.Guatemala, ("Guatemala", "GT") },
            { Countries.Guinea, ("Guinea", "GN") },
            { Countries.GuineaBissau, ("Guinea-Bissau", "GW") },
            { Countries.Guyana, ("Guyana", "GY") },
            
            // H
            { Countries.Haiti, ("Haiti", "HT") },
            { Countries.Honduras, ("Honduras", "HN") },
            { Countries.Hungary, ("Hungary", "HU") },
            
            // I
            { Countries.Iceland, ("Iceland", "IS") },
            { Countries.India, ("India", "IN") },
            { Countries.Indonesia, ("Indonesia", "ID") },
            { Countries.Iran, ("Iran", "IR") },
            { Countries.Iraq, ("Iraq", "IQ") },
            { Countries.Ireland, ("Ireland", "IE") },
            { Countries.Israel, ("Israel", "IL") },
            { Countries.Italy, ("Italy", "IT") },
            { Countries.IvoryCoast, ("Côte d'Ivoire", "CI") },
            
            // J
            { Countries.Jamaica, ("Jamaica", "JM") },
            { Countries.Japan, ("Japan", "JP") },
            { Countries.Jordan, ("Jordan", "JO") },
            
            // K
            { Countries.Kazakhstan, ("Kazakhstan", "KZ") },
            { Countries.Kenya, ("Kenya", "KE") },
            { Countries.Kiribati, ("Kiribati", "KI") },
            { Countries.Kuwait, ("Kuwait", "KW") },
            { Countries.Kyrgyzstan, ("Kyrgyzstan", "KG") },
            
            // L
            { Countries.Laos, ("Laos", "LA") },
            { Countries.Latvia, ("Latvia", "LV") },
            { Countries.Lebanon, ("Lebanon", "LB") },
            { Countries.Lesotho, ("Lesotho", "LS") },
            { Countries.Liberia, ("Liberia", "LR") },
            { Countries.Libya, ("Libya", "LY") },
            { Countries.Liechtenstein, ("Liechtenstein", "LI") },
            { Countries.Lithuania, ("Lithuania", "LT") },
            { Countries.Luxembourg, ("Luxembourg", "LU") },
            
            // M
            { Countries.Madagascar, ("Madagascar", "MG") },
            { Countries.Malawi, ("Malawi", "MW") },
            { Countries.Malaysia, ("Malaysia", "MY") },
            { Countries.Maldives, ("Maldives", "MV") },
            { Countries.Mali, ("Mali", "ML") },
            { Countries.Malta, ("Malta", "MT") },
            { Countries.MarshallIslands, ("Marshall Islands", "MH") },
            { Countries.Mauritania, ("Mauritania", "MR") },
            { Countries.Mauritius, ("Mauritius", "MU") },
            { Countries.Mexico, ("Mexico", "MX") },
            { Countries.Micronesia, ("Micronesia", "FM") },
            { Countries.Moldova, ("Moldova", "MD") },
            { Countries.Monaco, ("Monaco", "MC") },
            { Countries.Mongolia, ("Mongolia", "MN") },
            { Countries.Montenegro, ("Montenegro", "ME") },
            { Countries.Morocco, ("Morocco", "MA") },
            { Countries.Mozambique, ("Mozambique", "MZ") },
            { Countries.Myanmar, ("Myanmar", "MM") },
            
            // N
            { Countries.Namibia, ("Namibia", "NA") },
            { Countries.Nauru, ("Nauru", "NR") },
            { Countries.Nepal, ("Nepal", "NP") },
            { Countries.Netherlands, ("Netherlands", "NL") },
            { Countries.NewZealand, ("New Zealand", "NZ") },
            { Countries.Nicaragua, ("Nicaragua", "NI") },
            { Countries.Niger, ("Niger", "NE") },
            { Countries.Nigeria, ("Nigeria", "NG") },
            { Countries.NorthKorea, ("North Korea", "KP") },
            { Countries.NorthMacedonia, ("North Macedonia", "MK") },
            { Countries.Norway, ("Norway", "NO") },
            
            // O
            { Countries.Oman, ("Oman", "OM") },
            
            // P
            { Countries.Pakistan, ("Pakistan", "PK") },
            { Countries.Palau, ("Palau", "PW") },
            { Countries.Palestine, ("Palestine", "PS") },
            { Countries.Panama, ("Panama", "PA") },
            { Countries.PapuaNewGuinea, ("Papua New Guinea", "PG") },
            { Countries.Paraguay, ("Paraguay", "PY") },
            { Countries.Peru, ("Peru", "PE") },
            { Countries.Philippines, ("Philippines", "PH") },
            { Countries.Poland, ("Poland", "PL") },
            { Countries.Portugal, ("Portugal", "PT") },
            
            // Q
            { Countries.Qatar, ("Qatar", "QA") },
            
            // R
            { Countries.Romania, ("Romania", "RO") },
            { Countries.Russia, ("Russia", "RU") },
            { Countries.Rwanda, ("Rwanda", "RW") },
            
            // S
            { Countries.SaintKittsAndNevis, ("Saint Kitts and Nevis", "KN") },
            { Countries.SaintLucia, ("Saint Lucia", "LC") },
            { Countries.SaintVincentAndTheGrenadines, ("Saint Vincent and the Grenadines", "VC") },
            { Countries.Samoa, ("Samoa", "WS") },
            { Countries.SanMarino, ("San Marino", "SM") },
            { Countries.SaoTomeAndPrincipe, ("São Tomé and Príncipe", "ST") },
            { Countries.SaudiArabia, ("Saudi Arabia", "SA") },
            { Countries.Senegal, ("Senegal", "SN") },
            { Countries.Serbia, ("Serbia", "RS") },
            { Countries.Seychelles, ("Seychelles", "SC") },
            { Countries.SierraLeone, ("Sierra Leone", "SL") },
            { Countries.Singapore, ("Singapore", "SG") },
            { Countries.Slovakia, ("Slovakia", "SK") },
            { Countries.Slovenia, ("Slovenia", "SI") },
            { Countries.SolomonIslands, ("Solomon Islands", "SB") },
            { Countries.Somalia, ("Somalia", "SO") },
            { Countries.SouthAfrica, ("South Africa", "ZA") },
            { Countries.SouthKorea, ("South Korea", "KR") },
            { Countries.SouthSudan, ("South Sudan", "SS") },
            { Countries.Spain, ("Spain", "ES") },
            { Countries.SriLanka, ("Sri Lanka", "LK") },
            { Countries.Sudan, ("Sudan", "SD") },
            { Countries.Suriname, ("Suriname", "SR") },
            { Countries.Sweden, ("Sweden", "SE") },
            { Countries.Switzerland, ("Switzerland", "CH") },
            { Countries.Syria, ("Syria", "SY") },
            
            // T
            { Countries.Tajikistan, ("Tajikistan", "TJ") },
            { Countries.Tanzania, ("Tanzania", "TZ") },
            { Countries.Thailand, ("Thailand", "TH") },
            { Countries.TimorLeste, ("Timor-Leste", "TL") },
            { Countries.Togo, ("Togo", "TG") },
            { Countries.Tonga, ("Tonga", "TO") },
            { Countries.TrinidadAndTobago, ("Trinidad and Tobago", "TT") },
            { Countries.Tunisia, ("Tunisia", "TN") },
            { Countries.Turkey, ("Turkey", "TR") },
            { Countries.Turkmenistan, ("Turkmenistan", "TM") },
            { Countries.Tuvalu, ("Tuvalu", "TV") },
            
            // U
            { Countries.Uganda, ("Uganda", "UG") },
            { Countries.Ukraine, ("Ukraine", "UA") },
            { Countries.UnitedArabEmirates, ("United Arab Emirates", "AE") },
            { Countries.UnitedKingdom, ("United Kingdom", "GB") },
            { Countries.UnitedStates, ("United States", "US") },
            { Countries.Uruguay, ("Uruguay", "UY") },
            { Countries.Uzbekistan, ("Uzbekistan", "UZ") },
            
            // V
            { Countries.Vanuatu, ("Vanuatu", "VU") },
            { Countries.VaticanCity, ("Vatican City", "VA") },
            { Countries.Venezuela, ("Venezuela", "VE") },
            { Countries.Vietnam, ("Vietnam", "VN") },
            
            // Y
            { Countries.Yemen, ("Yemen", "YE") },
            
            // Z
            { Countries.Zambia, ("Zambia", "ZM") },
            { Countries.Zimbabwe, ("Zimbabwe", "ZW") }
        };

        /// <summary>
        /// Gets the official name of the country
        /// </summary>
        /// <param name="country">The country enum value</param>
        /// <returns>The official name of the country</returns>
        public static string GetName(this Countries country)
        {
            return CountryData.TryGetValue(country, out var data) ? data.Name : country.ToString();
        }

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code
        /// </summary>
        /// <param name="country">The country enum value</param>
        /// <returns>The two-letter ISO country code</returns>
        public static string GetIsoCode(this Countries country)
        {
            return CountryData.TryGetValue(country, out var data) ? data.IsoCode : string.Empty;
        }
    }
}