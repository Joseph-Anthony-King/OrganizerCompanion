using OrganizerCompanion.Core.Interfaces.Type;

namespace OrganizerCompanion.Core.Models.Type
{
    internal class State : IState
    {
        public string? Name { get; set; } = null;
        public string? Abbreviation { get; set; } = null;
    }
}
