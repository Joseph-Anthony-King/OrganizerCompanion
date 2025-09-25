namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IPassword : IDomainEntity
    {
        string? PasswordValue { get; set; }
        string? PasswordHint { get; set; }
        int AccountId { get; set; }
        IAccount? Account { get; set; }
    }
}
