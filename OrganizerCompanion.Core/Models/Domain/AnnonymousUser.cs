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
            DateTime? dateModified)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfNegative(id, nameof(id));

                _id = id;
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
        public IDomainEntity Cast<T>() => throw new NotImplementedException();
        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        #endregion
    }
}
