using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Validation.Attributes;

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
        private List<AccountFeature> _features = [];
        private List<SubAccount>? _subAccounts = null;
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        [JsonIgnore]
        List<IAccountFeature> IAccount.Features
        {
            get => _features.ConvertAll(feature => (IAccountFeature)feature);
            set
            {
                _features = value.ConvertAll(feature => (AccountFeature)feature);
                DateModified = DateTime.Now;
            }
        }

        [JsonIgnore]
        List<ISubAccount>? IAccount.Accounts
        {
            get => _subAccounts?.ConvertAll(account => (ISubAccount)account);
            set
            {
                _subAccounts = value?.ConvertAll(account => (SubAccount)account);
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
                    throw new ArgumentOutOfRangeException(nameof(Id), "Id must be a non-negative number.");
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

        [Required, JsonPropertyName("license"), GuidValidator]
        public string? License
        {
            get => _license;
            set
            {
                _license = value;
                DateModified = DateTime.Now;
            }
        }    

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

        [JsonPropertyName("accounts"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<SubAccount>? Accounts
        {
            get => _subAccounts;
            set
            {
                _subAccounts ??= [];
                _subAccounts = value?.ConvertAll(account => account);
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated => _dateCreated;

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
            List<AccountFeature> features,
            List<SubAccount> accounts,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            _id = id;
            _accountName = accountName;
            _accountNumber = accountNumber;
            _license = license;
            _features = features;
            _subAccounts = accounts;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }

        public Account(
            string? accountName,
            string? accountNumber,
            string? license,
            List<AccountFeature> features,
            List<SubAccount>? accounts)
        {
            _accountName = accountName;
            _accountNumber = accountNumber;
            _license = license;
            _features = features;
            _subAccounts = accounts;
        }

        public Account(IAccountDTO dto)
        {
            _id = dto.Id;
            _accountName = dto.AccountName;
            _accountNumber = dto.AccountNumber;
            _license = dto.License;
            _features = dto.Features.ConvertAll(featureDTO => new AccountFeature(featureDTO, dto.Id));
            _subAccounts = dto.Accounts?.ConvertAll(subAccountDTO => new SubAccount(subAccountDTO));
            _dateCreated = dto.DateCreated;
            DateModified = dto.DateModified;
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
                        License = this.License,
                        Features = this.Features.ConvertAll(feature => feature.Cast<FeatureDTO>()),
                        Accounts = this.Accounts?.ConvertAll(account => account.Cast<SubAccountDTO>()),
                        DateCreated = this.DateCreated,
                        DateModified = this.DateModified
                    };
                    return (T)dto;
                }
                else throw new InvalidCastException($"Cannot cast Account to type {typeof(T).Name}.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.AccountName:{1}", _id, _accountName);
        #endregion
    }
}
