using System.ComponentModel.DataAnnotations;
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
        private Account? _account = null;
        private Feature? _feature = null;
        private readonly DateTime _dateCreated = DateTime.UtcNow;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        IFeature? IAccountFeature.Feature
        {
            get => Feature;
            set
            {
                Feature = (Feature)value!;
                DateModified = DateTime.Now;
            }
        }
        IAccount? IAccountFeature.Account
        {
            get => Account;
            set
            {
                Account = (Account)value!;
                DateModified = DateTime.Now;
            }
        }
        #endregion

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
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
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account Id must be a non-negative number.")]
        public int AccountId
        {
            get => _account?.Id ?? 0;
        }

        [JsonIgnore]
        public Account? Account
        {
            get => _account;
            set
            {
                _account = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("featureId"), Range(0, int.MaxValue, ErrorMessage = "Feature Id must be a non-negative number.")]
        public int FeatureId
        {
            get => _feature?.Id ?? 0;
        }

        [JsonIgnore]
        public Feature? Feature
        {
            get => _feature;
            set
            {
                _feature = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated => _dateCreated;

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public AccountFeature() { }

        [JsonConstructor]
        public AccountFeature(
            int id,
            Account? account,
            Feature? feature,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            _id = id;
            _account = account;
            _feature = feature;
            _dateCreated = dateCreated;
            DateModified = dateModified;
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
            _feature = (Feature)feature;
        }

        public AccountFeature(
            IAccountDTO accountDTO,
            IFeatureDTO featureDTO)
        {
            if (accountDTO == null)
                throw new ArgumentNullException(nameof(accountDTO), "AccountDTO cannot be null.");

            if (featureDTO == null)
                throw new ArgumentNullException(nameof(featureDTO), "FeatureDTO cannot be null.");

            // Create lightweight objects without cascading child object creation
            // Feature has a constructor that takes IFeatureDTO
            _feature = new Feature(featureDTO);

            // Create Account using JsonConstructor with empty collections to avoid recursion
            _account = new Account(
                id: accountDTO.Id,
                accountName: accountDTO.AccountName,
                accountNumber: accountDTO.AccountNumber,
                license: accountDTO.License,
                features: [], // Empty to avoid infinite recursion
                accounts: accountDTO.Accounts?.ConvertAll(sa => new SubAccount(sa)) ?? [],
                dateCreated: accountDTO.DateCreated,
                dateModified: accountDTO.DateModified);
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
