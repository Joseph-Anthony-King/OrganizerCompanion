using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IAnnonymousUserDTO : IDomainEntity
    {
        int AccountId { get; set; }
        string UserName { get; set; }
        bool? IsCast { get; set; }
        int? CastId { get; set; }
        string? CastType { get; set; }
    }
}
