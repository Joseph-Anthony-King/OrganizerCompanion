using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        private DateTime _createdDate = DateTime.UtcNow;
        #endregion

        #region Properties
        [Key]
        [Column("FeatureId")]
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

        [Required, JsonPropertyName("featureName")]
        public string? FeatureName
        {
            get => _featureName;
            set
            {
                _featureName = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("isEnabled")]
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate => _createdDate;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;
        #endregion

        #region Constructors
        public Feature() { }

        [JsonConstructor]
        public Feature(
            int id,
            string? featureName,
            bool isEnabled,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _featureName = featureName;
            _isEnabled = isEnabled;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
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
            _createdDate = dto.CreatedDate;
            ModifiedDate = dto.ModifiedDate;
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
                        CreatedDate = CreatedDate,
                        ModifiedDate = ModifiedDate,
                    };
                    return (T)dto;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast Feature to type {typeof(T).Name}.");
                }
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
