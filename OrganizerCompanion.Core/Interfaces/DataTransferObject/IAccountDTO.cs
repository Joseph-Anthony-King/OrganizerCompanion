using OrganizerCompanion.Core.Models.Type;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IAccountDTO
    {
        int Id { get; set; }
        string? AccountName { get; set; }
        string? AccountNumber { get; set; }
        string? License { get; set; }
        DatabaseConnection? DatabaseConnection { get; set; }
        List<IFeatureDTO> Features { get; set; }
    }
}
