using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Extensions
{
    internal static class StateExtensions
    {
        private static readonly Dictionary<States, (string Name, string Abbreviation)> StateData = new()
        {
            { States.Alabama, ("Alabama", "AL") },
            { States.Alaska, ("Alaska", "AK") },
            { States.Arizona, ("Arizona", "AZ") },
            { States.Arkansas, ("Arkansas", "AR") },
            { States.California, ("California", "CA") },
            { States.Colorado, ("Colorado", "CO") },
            { States.Connecticut, ("Connecticut", "CT") },
            { States.Delaware, ("Delaware", "DE") },
            { States.Florida, ("Florida", "FL") },
            { States.Georgia, ("Georgia", "GA") },
            { States.Hawaii, ("Hawaii", "HI") },
            { States.Idaho, ("Idaho", "ID") },
            { States.Illinois, ("Illinois", "IL") },
            { States.Indiana, ("Indiana", "IN") },
            { States.Iowa, ("Iowa", "IA") },
            { States.Kansas, ("Kansas", "KS") },
            { States.Kentucky, ("Kentucky", "KY") },
            { States.Louisiana, ("Louisiana", "LA") },
            { States.Maine, ("Maine", "ME") },
            { States.Maryland, ("Maryland", "MD") },
            { States.Massachusetts, ("Massachusetts", "MA") },
            { States.Michigan, ("Michigan", "MI") },
            { States.Minnesota, ("Minnesota", "MN") },
            { States.Mississippi, ("Mississippi", "MS") },
            { States.Missouri, ("Missouri", "MO") },
            { States.Montana, ("Montana", "MT") },
            { States.Nebraska, ("Nebraska", "NE") },
            { States.Nevada, ("Nevada", "NV") },
            { States.NewHampshire, ("New Hampshire", "NH") },
            { States.NewJersey, ("New Jersey", "NJ") },
            { States.NewMexico, ("New Mexico", "NM") },
            { States.NewYork, ("New York", "NY") },
            { States.NorthCarolina, ("North Carolina", "NC") },
            { States.NorthDakota, ("North Dakota", "ND") },
            { States.Ohio, ("Ohio", "OH") },
            { States.Oklahoma, ("Oklahoma", "OK") },
            { States.Oregon, ("Oregon", "OR") },
            { States.Pennsylvania, ("Pennsylvania", "PA") },
            { States.RhodeIsland, ("Rhode Island", "RI") },
            { States.SouthCarolina, ("South Carolina", "SC") },
            { States.SouthDakota, ("South Dakota", "SD") },
            { States.Tennessee, ("Tennessee", "TN") },
            { States.Texas, ("Texas", "TX") },
            { States.Utah, ("Utah", "UT") },
            { States.Vermont, ("Vermont", "VT") },
            { States.Virginia, ("Virginia", "VA") },
            { States.Washington, ("Washington", "WA") },
            { States.WestVirginia, ("West Virginia", "WV") },
            { States.Wisconsin, ("Wisconsin", "WI") },
            { States.Wyoming, ("Wyoming", "WY") }
        };

        public static string GetName(this States state)
        {
            return StateData.TryGetValue(state, out var data) ? data.Name : state.ToString();
        }

        public static string GetAbbreviation(this States state)
        {
            return StateData.TryGetValue(state, out var data) ? data.Abbreviation : string.Empty;
        }

        public static Models.Type.State ToStateModel(this States state)
        {
            return new Models.Type.State
            {
                Name = state.GetName(),
                Abbreviation = state.GetAbbreviation()
            };
        }
    }
}