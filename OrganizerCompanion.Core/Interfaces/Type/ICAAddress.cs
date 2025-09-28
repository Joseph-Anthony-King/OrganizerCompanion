namespace OrganizerCompanion.Core.Interfaces.Type
{
    internal interface ICAAddress
    {
        string? Street1 { get; set; }
        string? Street2 { get; set; }
        string? City { get; set; }
        ISubNationalSubdivision? Province { get; set; }
        string? ZipCode { get; set; }
        string? Country { get; set; }
    }
}
