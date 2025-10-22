namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IDatabaseConnection : IDomainEntity, Type.IDatabaseConnection
    {
        int AccountId { get; }
        IAccount Account { get; set; }
    }
}
