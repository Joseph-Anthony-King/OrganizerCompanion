using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class AnnonymousUserDTO : IAnnonymousUserDTO
    {
        #region Explicit Interface Implementations
        [JsonIgnore]
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }
        public string ToJson()
        {
            throw new NotImplementedException();
        }
        #endregion

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "ID must be a non-negative number")]
        public int Id { get; set; } = 0;
        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "ID must be a non-negative number")]
        public int AccountId { get; set; } = 0;
        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;
    }
}
