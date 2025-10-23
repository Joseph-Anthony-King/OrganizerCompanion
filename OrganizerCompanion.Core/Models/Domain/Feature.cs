using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class Feature : IFeature
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string? _featureName = null;
        private bool _isEnabled = false;
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
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

        [Required, JsonPropertyName("featureName")]
        public string? FeatureName
        {
            get => _featureName;
            set
            {
                _featureName = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("isEnabled")]
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated => _dateCreated;

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public Feature() { }

        [JsonConstructor]
        public Feature(
            int id,
            string? featureName,
            bool isEnabled,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            _id = id;
            _featureName = featureName;
            _isEnabled = isEnabled;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }

        public Feature(
            string? featureName,
            bool isEnabled)
        {
            _featureName = featureName;
            _isEnabled = isEnabled;
        }
        
        public Feature(IFeatureDTO dto)
        {
            _id = dto.Id;
            _featureName = dto.FeatureName;
            _isEnabled = dto.IsEnabled;
            _dateCreated = dto.DateCreated;
            DateModified = dto.DateModified;
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
                        FeatureName = FeatureName,
                        IsEnabled = IsEnabled,
                        DateCreated = DateCreated,
                        DateModified = DateModified,
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

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.FeatureName:{1}", _id, _featureName);
        #endregion
    }
}
