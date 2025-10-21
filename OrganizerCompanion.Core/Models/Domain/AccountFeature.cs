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
        private int _accountId = 0;
        private Account? _account = null;
        private int _featureId = 0;
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
                _id = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account Id must be a non-negative number.")]
        public int AccountId
        {
            get => _accountId;
            set
            {
                _accountId = value;
                DateModified = DateTime.Now;
            }
        }

        [JsonIgnore]
        public Account? Account
        {
            get => _account;
            set
            {
                _account = value;
                _accountId = value?.Id ?? 0;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("featureId"), Range(0, int.MaxValue, ErrorMessage = "Feature Id must be a non-negative number.")]
        public int FeatureId
        {
            get => _featureId;
            set
            {
                _featureId = value;
                DateModified = DateTime.Now;
            }
        }

        [JsonIgnore]
        public Feature? Feature
        {
            get => _feature;
            set
            {
                _feature = value;
                _featureId = value?.Id ?? 0;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get => _dateCreated; }

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public AccountFeature() { }

        [JsonConstructor]
        public AccountFeature(
            int id,
            int accountId, 
            int featureId,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            _id = id;
            _accountId = accountId;
            _featureId = featureId;
            _dateCreated = dateCreated;
            DateModified = dateModified;
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
                        Id = this.Id,
                        FeatureName = this.Feature!.FeatureName!,
                        IsEnabled = this.Feature!.IsEnabled!,
                    };
                    return (T)dto;
                }
                else throw new InvalidCastException($"Cannot cast Feature to type {typeof(T).Name}.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        #endregion
    }
}
