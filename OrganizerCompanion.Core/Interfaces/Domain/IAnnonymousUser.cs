namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IAnnonymousUser : IDomainEntity
    {
        string UserName { get; set; }
        int AccountId { get; set; }
        ISubAccount? Account { get; set; }
        bool? IsCast { get; set; }
        int? CastId { get; set; }
        string? CastType { get; set; }
    }
}
