using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class SubAccount : ISubAccount
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private int _linkedEntityId = 0;
        private string? _linkedEntityType = null;
        private IDomainEntity? _linkedEntity = null;
        private int? _accountId = null;
        private IAccount? _account = null;
        private DateTime _dateCreated = DateTime.UtcNow;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        [JsonIgnore]
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Id must be a non-negative number.");
                _id = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("linkedEntityId"), Range(0, int.MaxValue, ErrorMessage = "Linked Entity Id must be a non-negative number.")]
        public int LinkedEntityId
        {
            get => _linkedEntityId;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Linked Entity Id must be a non-negative number.");
                _linkedEntityId = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("linktedEntityType"), MaxLength(50, ErrorMessage = "Linked Account Id cannot exceed 50 characters.")]
        public string? LinkedEntityType
        {
            get => _linkedEntityType;
        }

        [Required, JsonPropertyName("linkedEntity")]
        public IDomainEntity? LinkedEntity
        {
            get => _linkedEntity;
            set
            {
                _linkedEntity = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account Id must be a non-negative number.")]
        public int? AccountId
        {
            get => _accountId;
            set
            {
                if (value != null && value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Account Id must be a non-negative number.");
                _accountId = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("account")]
        public IAccount? Account
        {
            get => _account;
            set
            {
                _account = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get => _dateCreated; }

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Constructors
        public SubAccount() { }

        [JsonConstructor]
        public SubAccount(
            int id, 
            int linkedEntityId, 
            string? linkedEntityType, 
            IDomainEntity? linkedEntity, 
            int? accountId, 
            IAccount? account, 
            DateTime dateCreated, 
            DateTime? dateModified)
        {
            Id = id;
            LinkedEntityId = linkedEntityId;
            _linkedEntityType = linkedEntityType;
            LinkedEntity = linkedEntity;
            AccountId = accountId;
            Account = account;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }

        public SubAccount(
            IDomainEntity? linkedEntity,
            IAccount? account)
        {
            _linkedEntityId = linkedEntity?.Id ?? 0;
            _linkedEntityType = linkedEntity?.GetType().Name;
            LinkedEntity = linkedEntity;
            _accountId = account?.Id;
            Account = account;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(SubAccountDTO) || typeof(T) == typeof(ISubAccountDTO))
                {
                    var dto = new SubAccountDTO(
                        _id,
                        _linkedEntityId,
                        _linkedEntityType,
                        _linkedEntity?.Cast<IDomainEntity>(),
                        _accountId,
                        _account!.Cast<AccountDTO>(),
                        _dateCreated,
                        DateModified);
                    return (T)(IDomainEntity)dto;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast SubAccount to type {typeof(T).Name}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.LinkedEntityId:{1}.LinkedEntityType:{2}", _id, _linkedEntityId, _linkedEntityType);
        #endregion
    }
}
