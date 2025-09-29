namespace OrganizerCompanion.Core.Interfaces.Type
{
    internal interface IMXAddress : IAddress
    {
        string? Street { get; set; }
        string? Neighborhood { get; set; }
        string? PostalCode { get; set; }
        string? City { get; set; }
        ISubNationalSubdivision? State { get; set; }
        string? Country { get; set; }
    }
}
