using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    public interface IEmailDTO : IDomainEntity, Type.IEmail
    {
        bool IsConfirmed { get; set; }
    }
}
