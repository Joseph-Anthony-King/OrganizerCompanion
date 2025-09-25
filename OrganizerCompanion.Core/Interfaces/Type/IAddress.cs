using OrganizerCompanion.Core.Enums;
namespace OrganizerCompanion.Core.Interfaces.Type
{
    internal interface IAddress : IType
    {
        Types? Type { get; set; }
    }
}
