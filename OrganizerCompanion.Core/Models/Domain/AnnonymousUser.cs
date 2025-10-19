using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class AnnonymousUser : IAnnonymousUser
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private int _accountId = 0;
        private Account? _account = null;
        private bool _isCast = false;
        private int _castId = 0;
        private string? _castType = null;
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        IAccount? IAnnonymousUser.Account
        {
            get => _account;
            set
            {
                _account = (Account?)value;
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

        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account Id must be a non-negative number."), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int AccountId
        {
            get => _accountId;
            set
            {
                _accountId = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("account"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Account? Account
        {
            get => _account;
            set
            {
                _account = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("isCast")]
        public bool IsCast
        {
            get => _isCast;
            set
            {
                _isCast = value;
                DateModified = DateTime.Now;
            }
        }

        [JsonPropertyName("castId"), Range(0, int.MaxValue, ErrorMessage = "Converted Id must be a non-negative number."), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int CastId
        {
            get => _castId;
            set
            {
                _castId = value;
                DateModified = DateTime.Now;
            }
        }

        [JsonPropertyName("castType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CastType
        {
            get => _castType;
            set
            {
                _castType = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get => _dateCreated; }

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public AnnonymousUser() { }

        [JsonConstructor]
        public AnnonymousUser(
            int id,
            DateTime dateCreated,
            DateTime? dateModified,
            bool? isCast = null,
            int? castId = null,
            string? castType = null)
        {
            try
            {
                _id = id;
                _isCast = isCast != null && (bool)isCast;
                _castId = castId != null ? (int)castId : 0;
                _castType = castType;
                _dateCreated = dateCreated;
                DateModified = dateModified;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error creating AnnonymousUser object.", ex);
            }
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(Organization))
                {
                    var result = (T)(IDomainEntity)new Organization(
                        0,
                        null,
                        [],
                        [],
                        [],
                        [],
                        [],
                        [],
                        this.DateCreated,
                        this.DateModified
                    );

                    IsCast = true;
                    CastId = result.Id;
                    CastType = nameof(Organization);

                    return result;
                }
                else if (typeof(T) == typeof(User))
                {
                    var result = (T)(IDomainEntity)new User(
                        0,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        [],
                        [],
                        [],
                        null,
                        null,
                        null,
                        null,
                        this.DateCreated,
                        this.DateModified
                    );

                    IsCast = true;
                    CastId = result.Id;
                    CastType = nameof(User);

                    return result;
                }
                else if (typeof(T) == typeof(AnnonymousUserDTO))
                {
                    var result = (T)(IDomainEntity)new AnnonymousUserDTO
                    {
                        Id = this.Id,
                        AccountId = this.AccountId,
                        DateCreated = this.DateCreated,
                        DateModified = this.DateModified
                    };

                    return result;
                }
                else throw new InvalidCastException($"Cannot cast AnnonymousUser to {typeof(T).Name}.");
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
