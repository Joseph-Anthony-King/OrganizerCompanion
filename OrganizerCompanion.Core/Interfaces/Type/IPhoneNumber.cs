using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Interfaces.Type
{
    internal interface IPhoneNumber
    {
        string? Phone { get; set; }
        Types? Type { get; set; }
    }
}
