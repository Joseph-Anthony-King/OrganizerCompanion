using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class FeatureDTO : IFeatureDTO
    {
        #region Explicit Interface Implementations
        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public string ToJson()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
        [Required, JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [Required, JsonPropertyName("featureName")]
        public string? FeatureName { get; set; } = null;

        [Required, JsonPropertyName("isEnabled")]
        public bool IsEnabled { get; set; } = false;

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = default;
        #endregion
    }
}
