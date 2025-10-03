using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class Account : IAccount
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string? _accountName = null;
        private string? _accountNumber = null;
        private string? _license = null;
        private string? _databaseConnection = null;
        private SupportedDatabases? _databaseType = null;
        private int _linkedEntityId = 0;
        private string? _linkedEntityType = null;
        private IDomainEntity? _linkedEntity = null;
        private List<AccountFeature> _features = [];
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        List<IAccountFeature> IAccount.Features
        {
            get => _features.ConvertAll(feature => (Interfaces.Domain.IAccountFeature)feature);
            set
            {
                _features = value.ConvertAll(feature => (AccountFeature)feature);
                DateModified = DateTime.Now;
            }
        }
        #endregion

        [Required, JsonPropertyName("id"), Range(1, int.MaxValue, ErrorMessage = "ID must be a positive number")]
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("accountName")]
        public string? AccountName
        {
            get => _accountName;
            set
            {
                _accountName = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("accountNumber")]
        public string? AccountNumber
        {
            get => _accountNumber;
            set
            {
                _accountNumber = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("license")]
        public string? License
        {
            get => _license;
            set
            {
                _license = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("databaseConnection")]
        public string? DatabaseConnection
        {
            get => _databaseConnection;
            set
            {
                _databaseConnection = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("databaseType")]
        public SupportedDatabases? DatabaseType
        {
            get => _databaseType;
            set
            {
                _databaseType = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("linkedEntityId"), Range(1, int.MaxValue, ErrorMessage = "ID must be a positive number")]
        public int LinkedEntityId
        {
            get => _linkedEntityId;
            set
            {
                _linkedEntityId = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("linkedEntity")]
        public IDomainEntity? LinkedEntity
        {
            get => _linkedEntity;
            set
            {
                _linkedEntity = value;
                _linkedEntityType = value?.GetType().Name;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("linkedEntityType")]
        public string? LinkedEntityType => _linkedEntityType;
        

        [Required, JsonPropertyName("features")]
        public List<AccountFeature> Features
        {
            get => _features;
            set
            {
                _features = value;
                DateModified = DateTime.Now;
            }
        }

        [JsonIgnore]
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get => _dateCreated; }

        [JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public Account() { }

        [JsonConstructor]
        public Account(
            int id,
            string? accountName,
            string? accountNumber,
            string? license,
            string? databaseConnection,
            SupportedDatabases? databaseType,
            int linkedEntityId,
            IDomainEntity? linkedEntity,
            List<AccountFeature> features,
            DateTime dateCreated,
            DateTime? dateModified,
            bool? isCast = null,
            int? castId = null,
            string? castType = null)
        {
            try
            {
                _id = id;
                _accountName = accountName;
                _accountNumber = accountNumber;
                _license = license;
                _databaseConnection = databaseConnection;
                _databaseType = databaseType;
                _linkedEntityId = linkedEntityId;
                _linkedEntity = linkedEntity;
                _linkedEntityType = linkedEntity?.GetType().Name;
                _features = features;
                _dateCreated = dateCreated;
                DateModified = dateModified;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating Account object", ex);
            }
        }

        public Account(
            string? accountName,
            string? accountNumber,
            string? license,
            string? databaseConnection,
            SupportedDatabases? databaseType,
            IDomainEntity linkedEntity,
            List<AccountFeature> features,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            try
            {
                _accountName = accountName;
                _accountNumber = accountNumber;
                _license = license;
                _databaseConnection = databaseConnection;
                _databaseType = databaseType;
                _linkedEntityId = linkedEntity.Id;
                _linkedEntityType = linkedEntity.GetType().Name;
                _linkedEntity = linkedEntity;
                _features = features;
                _dateCreated = dateCreated;
                DateModified = dateModified;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating Account object", ex);
            }
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(AccountDTO) || typeof(T) == typeof(IAccountDTO))
                {
                    object dto = new AccountDTO()
                    {
                        Id = this.Id,
                        AccountName = this.AccountName,
                        AccountNumber = this.AccountNumber,
                        Features = this.Features.ConvertAll(feature => feature.Cast<FeatureDTO>())
                    };
                    return (T)dto;
                }
                else throw new InvalidCastException($"Cannot cast Account to type {typeof(T).Name}, casting is not supported for this type");
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Error casting Account to type {typeof(T).Name}: {ex.Message}", ex);
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.AccountName:{1}", _id, _accountName);
        #endregion
    }
}
