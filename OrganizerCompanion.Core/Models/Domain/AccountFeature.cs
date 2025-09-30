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

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "ID must be a non-negative number")]
        public int Id { get; set; } = 0;

        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account ID must be a non-negative number")]
        public int AccountId { get; set; } = 0;

        [JsonIgnore]
        public Account? Account { get; set; } = null;

        [Required, JsonPropertyName("featureId"), Range(0, int.MaxValue, ErrorMessage = "Account ID must be a non-negative number")]
        public int FeatureId { get; set; } = 0;

        [JsonIgnore]
        public Feature? Feature { get; set; } = null;

        [JsonIgnore]
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get => _dateCreated; }

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public AccountFeature() { }

        [JsonConstructor]
        public AccountFeature(int accountId, int featureId)
        {
            AccountId = accountId;
            FeatureId = featureId;
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
                else throw new InvalidCastException($"Cannot cast Feature to type {typeof(T).Name}, casting is not supported for this type");
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Error Feature Email to type {typeof(T).Name}: {ex.Message}", ex);
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        #endregion
    }
}
