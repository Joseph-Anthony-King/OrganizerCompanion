using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Registries;

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
        private IDomainEntity? _linkedEntity = null;
        private int? _accountId;
        private IAccount? _account = null;
        private readonly DateTime _createdDate = DateTime.UtcNow;

        // EF Core navigation properties
        private User? _user;
        private Organization? _organization;
        private AnnonymousUser? _annonymousUser;
        #endregion

        #region Properties
        [Key]
        [Column("SubAccountId")]
        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Id must be a non-negative number.");
                }

                _id = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("linkedEntity")]
        public IDomainEntity? LinkedEntity
        {
            get
            {
                if (_linkedEntity == null)
                {
                    if (_user != null)
                    {
                        _linkedEntity = _user;
                    }
                    else if (_organization != null)
                    {
                        _linkedEntity = _organization;
                    }
                    else if (_annonymousUser != null)
                    {
                        _linkedEntity = _annonymousUser;
                    }
                }
                return _linkedEntity;
            }
            set
            {
                _linkedEntity = value;

                // Clear all navigation properties first
                _user = null;
                User = null;
                UserId = null;
                _organization = null;
                Organization = null;
                OrganizationId = null;
                _annonymousUser = null;
                AnnonymousUser = null;
                AnnonymousUserId = null;

                // Set the appropriate navigation property based on type
                if (value is User user)
                {
                    _user = user;
                    User = user;
                    UserId = user.Id;
                }
                else if (value is Organization organization)
                {
                    _organization = organization;
                    Organization = organization;
                    OrganizationId = organization.Id;
                }
                else if (value is AnnonymousUser annonymousUser)
                {
                    _annonymousUser = annonymousUser;
                    AnnonymousUser = annonymousUser;
                    AnnonymousUserId = annonymousUser.Id;
                }

                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("linkedEntityId"), Range(0, int.MaxValue, ErrorMessage = "Linked Entity Id must be a non-negative number.")]
        public int LinkedEntityId
        {
            get => _linkedEntity?.Id ?? 0;
        }

        [NotMapped]
        [Required, JsonPropertyName("linktedEntityType"), MaxLength(50, ErrorMessage = "Linked Account Id cannot exceed 50 characters.")]
        public string? LinkedEntityType
        {
            get => _linkedEntity?.GetType().Name;
        }

        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account Id must be a non-negative number.")]
        public int? AccountId
        {
            get => _accountId;
            set => _accountId = value;
        }

        [ForeignKey(nameof(AccountId))]
        [Required, JsonPropertyName("account")]
        public IAccount? Account
        {
            get => _account;
            set
            {
                _account = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate => _createdDate;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;

        // Foreign key and navigation properties for EF Core
        [JsonIgnore]
        public int? UserId { get; set; }

        [JsonIgnore, ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [JsonIgnore]
        public int? OrganizationId { get; set; }

        [JsonIgnore, ForeignKey(nameof(OrganizationId))]
        public Organization? Organization { get; set; }

        [JsonIgnore]
        public int? AnnonymousUserId { get; set; }

        [JsonIgnore, ForeignKey(nameof(AnnonymousUserId))]
        public AnnonymousUser? AnnonymousUser { get; set; }
        #endregion

        #region Constructors
        public SubAccount() { }

        [JsonConstructor]
        public SubAccount(
            int id,
            IDomainEntity? linkedEntity,
            int? accountId,
            IAccount? account,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _linkedEntity = linkedEntity;
            _accountId = accountId;
            _account = account;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public SubAccount(
         IDomainEntity? linkedEntity,
            IAccount? account)
        {
            LinkedEntity = linkedEntity;
            Account = account;
            _accountId = account?.Id;
        }

        public SubAccount(ISubAccountDTO account)
        {
            _id = account.Id;
            _linkedEntity = account.LinkedEntity; // Already an IDomainEntity, no need to cast
            _accountId = account.AccountId;
            // For DTOs, we store the DTO itself which implements IAccount through IAccountDTO
            if (account.Account is IAccount accountDomain)
            {
                _account = accountDomain;
            }
            else if (account.Account != null)
            {
                // Create a new Account from the DTO
                _account = new Account(account.Account);
            }
            _createdDate = account.CreatedDate;
            ModifiedDate = account.ModifiedDate;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(SubAccountDTO) || typeof(T) == typeof(ISubAccountDTO))
                {
                    // Cast LinkedEntity using TypeRegistry approach if possible
                    IDomainEntity? castedLinkedEntity = null;
                    if (_linkedEntity != null && !string.IsNullOrEmpty(LinkedEntityType))
                    {
                        try
                        {
                            var targetTypeName = string.Format("{0}DTO", typeof(T).Name);
                            // Try to use TypeRegistry for LinkedEntity casting
                            var linkedEntityType = TypeRegistry.GetType(targetTypeName);
                            if (linkedEntityType != null)
                            {
                                // Use reflection to call the Cast method with the dynamic type
                                var castMethod = typeof(IDomainEntity).GetMethod("Cast")!.MakeGenericMethod(linkedEntityType);
                                castedLinkedEntity = (IDomainEntity)castMethod.Invoke(_linkedEntity, null)!;
                            }
                            else
                            {
                                // Fallback to original approach
                                castedLinkedEntity = _linkedEntity.Cast<IDomainEntity>();
                            }
                        }
                        catch
                        {
                            // If casting fails, use the original entity
                            castedLinkedEntity = _linkedEntity;
                        }
                    }

                    var dto = new SubAccountDTO(
                        _id,
                        LinkedEntityId,
                        LinkedEntityType,
                        castedLinkedEntity,
                        _accountId,
                        _account!.Cast<AccountDTO>(),
                        _createdDate,
                        ModifiedDate);

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

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.LinkedEntityId:{1}.LinkedEntityType:{2}", _id, LinkedEntityId, LinkedEntityType);
        #endregion
    }
}
