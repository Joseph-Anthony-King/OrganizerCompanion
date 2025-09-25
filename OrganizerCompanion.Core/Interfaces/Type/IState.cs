namespace OrganizerCompanion.Core.Interfaces.Type
{
    internal interface IState : IType
    {
        string? Name { get; set; }
        string? Abbreviation { get; set; }
    }
}
