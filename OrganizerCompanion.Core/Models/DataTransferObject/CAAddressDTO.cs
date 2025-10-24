using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Interfaces.Type;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class CAAddressDTO : ICAAddressDTO
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
        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id { get; set; } = 0;

        [Required, JsonPropertyName("features")]
        public List<FeatureDTO> Features { get; set; } = [];

        [Required, JsonPropertyName("street")]
        public string? Street1 { get; set; } = null;

        [Required, JsonPropertyName("street2")]
        public string? Street2 { get; set; } = null;

        [Required, JsonPropertyName("city")]
        public string? City { get; set; } = null;

        [Required, JsonPropertyName("province")]
        public INationalSubdivision? Province { get; set; } = null;

        [Required, JsonPropertyName("zipCode")]
        public string? ZipCode { get; set; } = null;

        [Required, JsonPropertyName("country")]
        public string? Country { get; set; } = null;

        [Required, JsonPropertyName("type")]
        public Types? Type { get; set; } = null;

        [Required, JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; } = false;

        [Required, JsonPropertyName("linkedEntity")]
        public IDomainEntity? LinkedEntity { get; set; } = null;

        [Required, JsonPropertyName("linkedEntityId")]
        public int? LinkedEntityId => LinkedEntity?.Id;

        [Required, JsonPropertyName("linkedEntityType")]
        public string? LinkedEntityType => LinkedEntity?.GetType().Name;

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = default;
        #endregion
    }
}
