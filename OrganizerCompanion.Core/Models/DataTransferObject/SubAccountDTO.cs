using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class SubAccountDTO : ISubAccountDTO
    {
        #region Fields
        private DateTime _dateCreated = DateTime.UtcNow;
        #endregion

        #region Explicit Interface Implementations
        [JsonIgnore]
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public string ToJson()
        {
            throw new NotImplementedException();
        }
        #endregion

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id { get; set; } = 0;
        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "AccountId must be a non-negative number.")]
        public int? AccountId { get; set; } = null;
        [Required, JsonPropertyName("account")]
        public IAccountDTO? Account { get; set; } = null;
        [Required, JsonPropertyName("linkedEntityId"), Range(0, int.MaxValue, ErrorMessage = "LinkedEntityId must be a non-negative number.")]
        public int LinkedEntityId { get; set; } = 0;
        [Required, JsonPropertyName("linkedEntityType")]
        public string? LinkedEntityType { get; } = null;
        [Required, JsonPropertyName("linkedEntity")]
        public IDomainEntity? LinkedEntity { get; set; } = null;
        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get => _dateCreated; }
        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;

        #region Constructors
        public SubAccountDTO() { }

        [JsonConstructor]
        public SubAccountDTO(
            int id,
            int linkedEntityId,
            string? linktedEntityType,
            IDomainEntity? linkedEntity,
            int? accountId,
            AccountDTO account,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            Id = id;
            LinkedEntityId = linkedEntityId;
            LinkedEntityType = linktedEntityType;
            LinkedEntity = linkedEntity;
            AccountId = accountId;
            Account = account;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }

        public SubAccountDTO(
            int id,
            IDomainEntity? linkedEntity,
            AccountDTO account,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            Id = id;
            LinkedEntityId = linkedEntity!.Id;
            LinkedEntityType = linkedEntity?.GetType().Name;
            LinkedEntity = linkedEntity;
            AccountId = account.Id;
            Account = account;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion
    }
}
