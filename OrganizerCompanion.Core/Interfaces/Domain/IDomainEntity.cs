namespace OrganizerCompanion.Core.Interfaces.Domain
{
    public interface IDomainEntity
    {
        int Id { get; set; }
        DateTime CreatedDate { get; }
        DateTime? ModifiedDate { get; set; }
        string ToJson();
        T Cast<T>() where T : IDomainEntity;
    }
}
