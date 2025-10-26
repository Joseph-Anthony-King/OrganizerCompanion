using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class AccountFeature : IAccountFeature
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private int? _accountId = null;
        private Account? _account = null;
        private int? _featureId = null;
        private Feature? _feature = null;
        [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        private DateTime _createdDate = DateTime.UtcNow; // Remove readonly
        #endregion

        #region Explicit Interface Implementations
        IAccount? IAccountFeature.Account
        {
            get => _account;
            set
            {
                _account = (Account?)value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        IFeature? IAccountFeature.Feature
        {
            get => _feature;
            set
            {
                _feature = (Feature?)value;
                ModifiedDate = DateTime.UtcNow;
            }
        }
        #endregion

        #region Properties
        [Key]
        [Column("AccountFeatureId")]
        [Required, JsonPropertyName("id")]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Id), "Id must be a non-negative number.");
                }
                _id = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("accountId")]
        [Range(0, int.MaxValue, ErrorMessage = "Account Id must be a non-negative number.")]
        public int? AccountId => _accountId ?? 0;

        [ForeignKey("AccountId")]
        [JsonIgnore] // Don't serialize the navigation property directly
        public virtual Account? Account
        {
            get => _account;
            set
            {
                if (value != null && value!.Id < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Account), "AccountId must be a non-negative number.");
                }
                _account = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("featureId")]
        [Range(0, int.MaxValue, ErrorMessage = "Feature Id must be a non-negative number.")]
        public int? FeatureId => _featureId ?? 0;

        [ForeignKey("FeatureId")]
        [JsonIgnore]
        public virtual Feature? Feature
        {
            get => _feature;
            set
            {
                if (value != null && value!.Id < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Feature), "FeatureId must be a non-negative number.");
                }
                _feature = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate => _createdDate;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;
        #endregion

        #region Constructors
        public AccountFeature() { }

        [JsonConstructor]
        public AccountFeature(
            int id,
            Account? account,
            Feature? feature,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _account = account;
            _accountId = account?.Id;
            _feature = feature;
            _featureId = feature?.Id;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;    
        }

        public AccountFeature(
            IAccount account,
            IFeature feature)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");
            }

            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature), "Feature cannot be null.");
            }

            _account = (Account)account;
            _accountId = _account.Id;
            _feature = (Feature)feature;
            _featureId = _feature.Id;
        }

        public AccountFeature(
            IAccountDTO accountDTO,
            IFeatureDTO featureDTO)
        {
            if (accountDTO == null)
                throw new ArgumentNullException(nameof(accountDTO), "AccountDTO cannot be null.");

            if (featureDTO == null)
                throw new ArgumentNullException(nameof(featureDTO), "FeatureDTO cannot be null.");

            _account = new Account(
                id: accountDTO.Id,
                accountName: accountDTO.AccountName,
                accountNumber: accountDTO.AccountNumber,
                license: accountDTO.License,
                features: [],
                accounts: accountDTO.Accounts?.ConvertAll(sa => new SubAccount(sa)) ?? [],
                createdDate: accountDTO.CreatedDate,
                modifiedDate: accountDTO.ModifiedDate);

            _accountId = _account.Id;

            _feature = new Feature(featureDTO);

            _featureId = _feature.Id;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(FeatureDTO) || typeof(T) == typeof(IFeatureDTO))
                {
                    object dto = new FeatureDTO
                    {
                        Id = Id,
                        FeatureName = Feature!.FeatureName!,
                        IsEnabled = Feature!.IsEnabled!,
                    };
                    return (T)dto;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast Feature to type {typeof(T).Name}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.AccountId:{1}.FeatureId:{2}", _id, AccountId, FeatureId);
        #endregion
    }
}
