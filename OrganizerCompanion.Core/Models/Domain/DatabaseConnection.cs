using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class DatabaseConnection : IDatabaseConnection
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string? _connectionString = null;
        private SupportedDatabases? _databaseType = null;
        private Account _account = null!;
        private DateTime _dateCreated = DateTime.UtcNow;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        IAccount IDatabaseConnection.Account
        {
            get => _account;
            set
            {
                _account = (Account)value;
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

        [Required, JsonPropertyName("connectionString")]
        public string? ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
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

        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "AccountId must be a non-negative number.")]
        public int AccountId => _account.Id;

        [Required, JsonPropertyName("account")]
        public Account Account
        {
            get => _account;
            set
            {
                _account = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated => _dateCreated;

        [JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public DatabaseConnection() { }

        [JsonConstructor]
        public DatabaseConnection(
            int id,
            string? connectionString,
            SupportedDatabases? databaseType,
            Account account,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            _id = id;
            _connectionString = connectionString;
            _databaseType = databaseType;
            _account = account;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.DatabaseType:{1}", _id, _databaseType);
        #endregion
    }
}
