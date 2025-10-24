namespace OrganizerCompanion.Core.Interfaces.Type
{
    internal interface IUSAddress : IAddress
    {
        string? Street1 { get; set; }
        string? Street2 { get; set; }
        string? City { get; set; }
        INationalSubdivision? State { get; set; }
        string? ZipCode { get; set; }
        string? Country { get; set; }
    }
}
