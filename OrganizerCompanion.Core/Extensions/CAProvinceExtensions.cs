using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Extensions
{
    internal static class CAProvinceExtensions
    {
        private static readonly Dictionary<CAProvinces, (string Name, string Abbreviation)> ProvinceData = new()
        {
            { CAProvinces.Alberta, ("Alberta", "AB") },
            { CAProvinces.BritishColumbia, ("British Columbia", "BC") },
            { CAProvinces.Manitoba, ("Manitoba", "MB") },
            { CAProvinces.NewBrunswick, ("New Brunswick", "NB") },
            { CAProvinces.NewfoundlandAndLabrador, ("Newfoundland and Labrador", "NL") },
            { CAProvinces.NovaScotia, ("Nova Scotia", "NS") },
            { CAProvinces.Ontario, ("Ontario", "ON") },
            { CAProvinces.PrinceEdwardIsland, ("Prince Edward Island", "PE") },
            { CAProvinces.Quebec, ("Quebec", "QC") },
            { CAProvinces.Saskatchewan, ("Saskatchewan", "SK") },
            { CAProvinces.NorthwestTerritories, ("Northwest Territories", "NT") },
            { CAProvinces.Nunavut, ("Nunavut", "NU") },
            { CAProvinces.Yukon, ("Yukon", "YT") }
        };

        public static string GetName(this CAProvinces province)
        {
            return ProvinceData.TryGetValue(province, out var data) ? data.Name : province.ToString();
        }

        public static string GetAbbreviation(this CAProvinces province)
        {
            return ProvinceData.TryGetValue(province, out var data) ? data.Abbreviation : string.Empty;
        }

        public static Models.Type.CAProvince ToStateModel(this CAProvinces province)
        {
            return new Models.Type.CAProvince
            {
                Name = province.GetName(),
                Abbreviation = province.GetAbbreviation()
            };
        }
    }
}
