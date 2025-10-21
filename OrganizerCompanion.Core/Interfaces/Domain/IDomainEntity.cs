namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IDomainEntity
    {
        int Id { get; set; }
        DateTime DateCreated { get; }
        DateTime? DateModified { get; set; }
        string ToJson();
        T Cast<T>() where T : IDomainEntity;
    }
}
