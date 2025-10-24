using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Interfaces.Type
{
    public interface IPhoneNumber
    {
        string? Phone { get; set; }
        Types? Type { get; set; }
        Countries? Country { get; set; }
    }
}
