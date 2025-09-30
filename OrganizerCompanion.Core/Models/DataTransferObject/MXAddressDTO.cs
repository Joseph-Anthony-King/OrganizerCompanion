using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Interfaces.Type;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class MXAddressDTO : IMXAddressDTO
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
        [Required, JsonPropertyName("street")]
        public string? Street { get; set; } = null;
        [Required, JsonPropertyName("neighborhood")]
        public string? Neighborhood { get; set; } = null;
        [Required, JsonPropertyName("postalCode")]
        public string? PostalCode { get; set; } = null;
        [Required, JsonPropertyName("city")]
        public string? City { get; set; } = null;
        [Required, JsonPropertyName("state")]
        public INationalSubdivision? State { get; set; } = null;
        [Required, JsonPropertyName("country")]
        public string? Country { get; set; } = null;
        [Required, JsonPropertyName("type")]
        public Types? Type { get; set; } = null;
    }
}
