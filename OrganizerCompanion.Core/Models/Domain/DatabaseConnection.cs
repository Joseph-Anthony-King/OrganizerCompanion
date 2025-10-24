using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        private DateTime _createdDate = DateTime.UtcNow;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        IAccount IDatabaseConnection.Account
        {
            get => _account;
            set
            {
                _account = (Account)value;
                ModifiedDate = DateTime.UtcNow;
            }
        }
        #endregion

        [Key]
        [Column("DatabaseConnectionId")]
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
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("connectionString")]
        public string? ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("databaseType")]
        public SupportedDatabases? DatabaseType
        {
            get => _databaseType;
            set
            {
                _databaseType = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "AccountId must be a non-negative number.")]
        public int AccountId => _account.Id;

        [ForeignKey("AccountId")]
        [Required, JsonPropertyName("account")]
        public Account Account
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
        #endregion

        #region Constructors
        public DatabaseConnection() { }

        [JsonConstructor]
        public DatabaseConnection(
            int id,
            string? connectionString,
            SupportedDatabases? databaseType,
            Account account,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _connectionString = connectionString;
            _databaseType = databaseType;
            _account = account;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public DatabaseConnection(
            string? connectionString,
            SupportedDatabases? databaseType,
            Account account)
        {
            _connectionString = connectionString;
            _databaseType = databaseType;
            _account = account;
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
