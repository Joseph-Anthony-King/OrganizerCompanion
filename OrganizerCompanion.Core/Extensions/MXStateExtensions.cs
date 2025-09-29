using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Extensions
{
    internal static class MXStateExtensions
    {
        private static readonly Dictionary<MXStates, (string Name, string Abbreviation)> StateData = new()
        {
            { MXStates.Aguascalientes, ("Aguascalientes", "AG") },
            { MXStates.BajaCalifornia, ("Baja California", "BC") },
            { MXStates.BajaCaliforniaSur, ("Baja California Sur", "BS") },
            { MXStates.Campeche, ("Campeche", "CM") },
            { MXStates.Chiapas, ("Chiapas", "CS") },
            { MXStates.Chihhuahua, ("Chihuahua", "CH") },
            { MXStates.CoahuilaDeZaragoza, ("Coahuila de Zaragoza", "CO") },
            { MXStates.Colima, ("Colima", "CL") },
            { MXStates.Durango, ("Durango", "DG") },
            { MXStates.Guanajuato, ("Guanajuato", "GT") },
            { MXStates.Guerrero, ("Guerrero", "GR") },
            { MXStates.Hidalgo, ("Hidalgo", "HG") },
            { MXStates.Jalisco, ("Jalisco", "JA") },
            { MXStates.México, ("México", "MX") },
            { MXStates.CiudadDeMéxico, ("Ciudad de México", "DF") },
            { MXStates.MichoacánDeOcampo, ("Michoacán de Ocampo", "MI") },
            { MXStates.Morelos, ("Morelos", "MO") },
            { MXStates.Nayarit, ("Nayarit", "NA") },
            { MXStates.NuevoLeón, ("Nuevo León", "NL") },
            { MXStates.Oaxaca, ("Oaxaca", "OA") },
            { MXStates.Puebla, ("Puebla", "PU") },
            { MXStates.Querétaro, ("Querétaro", "QT") },
            { MXStates.QuintanaRoo, ("Quintana Roo", "QR") },
            { MXStates.SanLuisPotosí, ("San Luis Potosí", "SL") },
            { MXStates.Sinaloa, ("Sinaloa", "SI") },
            { MXStates.Sonora, ("Sonora", "SO") },
            { MXStates.Tabasco, ("Tabasco", "TB") },
            { MXStates.Tamaulipas, ("Tamaulipas", "TM") },
            { MXStates.Tlaxcala, ("Tlaxcala", "TL") },
            { MXStates.VeracruzDeIgnacioDeLaLlave, ("Veracruz de Ignacio de la Llave", "VZ") },
            { MXStates.Yucatán, ("Yucatán", "YU") },
            { MXStates.Zacatecas, ("Zacatecas", "ZA") }
        };

        public static string GetName(this MXStates state)
        {
            return StateData.TryGetValue(state, out var data) ? data.Name : state.ToString();
        }

        public static string GetAbbreviation(this MXStates state)
        {
            return StateData.TryGetValue(state, out var data) ? data.Abbreviation : string.Empty;
        }

        public static Models.Type.MXState ToStateModel(this MXStates state)
        {
            return new Models.Type.MXState
            {
                Name = state.GetName(),
                Abbreviation = state.GetAbbreviation()
            };
        }
    }
}
