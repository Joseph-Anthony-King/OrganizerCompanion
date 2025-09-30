using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class FeatureDTO : IFeatureDTO
    {
        #region Explicit Interface Implementations
        [JsonIgnore]
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public DateTime DateCreated => throw new NotImplementedException();
        [JsonIgnore]
        public DateTime? DateModified { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }
        public string ToJson()
        {
            throw new NotImplementedException();
        }
        #endregion

        [Required, JsonPropertyName("id")]
        public int Id { get; set; } = 0;
        [Required, JsonPropertyName("featureName")]
        public string? FeatureName { get; set; } = null;
        [Required, JsonPropertyName("isEnabled")]
        public bool IsEnabled { get; set; } = false;
    }
}
