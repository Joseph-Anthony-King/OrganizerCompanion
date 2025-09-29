using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.Domain;

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
        private bool _isCast = false;
        private int _castId = 0;
        private string? _castType = null;
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
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

        [JsonPropertyName("castId"), Range(0, int.MaxValue, ErrorMessage = "Converted ID must be a non-negative number"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
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
                        dateCreated: this.DateCreated,
                        dateModified: this.DateModified
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
                else throw new Exception($"Conversion from AnnonymousUser to {typeof(T).Name} is not supported.");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error converting AnnonymousUser to {typeof(T).Name}.", ex);
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        #endregion
    }
}
