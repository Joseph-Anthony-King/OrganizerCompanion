using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Extensions
{
    internal static class USStateExtensions
    {
        private static readonly Dictionary<USStates, (string Name, string Abbreviation)> StateData = new()
        {
            { USStates.Alabama, ("Alabama", "AL") },
            { USStates.Alaska, ("Alaska", "AK") },
            { USStates.Arizona, ("Arizona", "AZ") },
            { USStates.Arkansas, ("Arkansas", "AR") },
            { USStates.California, ("California", "CA") },
            { USStates.Colorado, ("Colorado", "CO") },
            { USStates.Connecticut, ("Connecticut", "CT") },
            { USStates.Delaware, ("Delaware", "DE") },
            { USStates.Florida, ("Florida", "FL") },
            { USStates.Georgia, ("Georgia", "GA") },
            { USStates.Hawaii, ("Hawaii", "HI") },
            { USStates.Idaho, ("Idaho", "ID") },
            { USStates.Illinois, ("Illinois", "IL") },
            { USStates.Indiana, ("Indiana", "IN") },
            { USStates.Iowa, ("Iowa", "IA") },
            { USStates.Kansas, ("Kansas", "KS") },
            { USStates.Kentucky, ("Kentucky", "KY") },
            { USStates.Louisiana, ("Louisiana", "LA") },
            { USStates.Maine, ("Maine", "ME") },
            { USStates.Maryland, ("Maryland", "MD") },
            { USStates.Massachusetts, ("Massachusetts", "MA") },
            { USStates.Michigan, ("Michigan", "MI") },
            { USStates.Minnesota, ("Minnesota", "MN") },
            { USStates.Mississippi, ("Mississippi", "MS") },
            { USStates.Missouri, ("Missouri", "MO") },
            { USStates.Montana, ("Montana", "MT") },
            { USStates.Nebraska, ("Nebraska", "NE") },
            { USStates.Nevada, ("Nevada", "NV") },
            { USStates.NewHampshire, ("New Hampshire", "NH") },
            { USStates.NewJersey, ("New Jersey", "NJ") },
            { USStates.NewMexico, ("New Mexico", "NM") },
            { USStates.NewYork, ("New York", "NY") },
            { USStates.NorthCarolina, ("North Carolina", "NC") },
            { USStates.NorthDakota, ("North Dakota", "ND") },
            { USStates.Ohio, ("Ohio", "OH") },
            { USStates.Oklahoma, ("Oklahoma", "OK") },
            { USStates.Oregon, ("Oregon", "OR") },
            { USStates.Pennsylvania, ("Pennsylvania", "PA") },
            { USStates.RhodeIsland, ("Rhode Island", "RI") },
            { USStates.SouthCarolina, ("South Carolina", "SC") },
            { USStates.SouthDakota, ("South Dakota", "SD") },
            { USStates.Tennessee, ("Tennessee", "TN") },
            { USStates.Texas, ("Texas", "TX") },
            { USStates.Utah, ("Utah", "UT") },
            { USStates.Vermont, ("Vermont", "VT") },
            { USStates.Virginia, ("Virginia", "VA") },
            { USStates.Washington, ("Washington", "WA") },
            { USStates.WestVirginia, ("West Virginia", "WV") },
            { USStates.Wisconsin, ("Wisconsin", "WI") },
            { USStates.Wyoming, ("Wyoming", "WY") }
        };

        public static string GetName(this USStates state)
        {
            return StateData.TryGetValue(state, out var data) ? data.Name : state.ToString();
        }

        public static string GetAbbreviation(this USStates state)
        {
            return StateData.TryGetValue(state, out var data) ? data.Abbreviation : string.Empty;
        }

        public static Models.Type.USState ToStateModel(this USStates state)
        {
            return new Models.Type.USState
            {
                Name = state.GetName(),
                Abbreviation = state.GetAbbreviation()
            };
        }
    }
}