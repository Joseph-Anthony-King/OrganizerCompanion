using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Interfaces.Type
{
    public interface IEmail
    {
        string? EmailAddress { get; set; }
        Types? Type { get; set; }
        bool IsPrimary { get; set; }
    }
}
