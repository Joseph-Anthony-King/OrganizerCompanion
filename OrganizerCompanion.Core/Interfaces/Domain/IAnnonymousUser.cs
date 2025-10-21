namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IAnnonymousUser : IDomainEntity
    {
        int AccountId { get; set; }
        IAccount? Account { get; set; }
        bool? IsCast { get; set; }
        int? CastId { get; set; }
        string? CastType { get; set; }
    }
}
