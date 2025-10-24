using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class AnnonymousUserDTO : IAnnonymousUserDTO
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
        
        [Required, JsonPropertyName("userName"), MinLength(1, ErrorMessage = "User Name must be at least 1 character long."), MaxLength(100, ErrorMessage = "User Name cannot exceed 100 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account Id must be a non-negative number.")]
        public int AccountId { get; set; } = 0;

        [JsonPropertyName("isCast"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool? IsCast { get; set; } = false;

        [JsonPropertyName("castId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CastId { get; set; } = 0;

        [JsonPropertyName("castType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CastType { get; set; } = null;

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = default;
        #endregion
  }
}
