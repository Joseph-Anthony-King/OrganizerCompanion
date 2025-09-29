using OrganizerCompanion.Core.Interfaces.Type;

namespace OrganizerCompanion.Core.Models.Type
{
    internal class CAProvince : INationalSubdivision
    {
        public string? Name { get; set; } = null;
        public string? Abbreviation { get; set; } = null;
    }
}
