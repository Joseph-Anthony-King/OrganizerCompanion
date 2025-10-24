using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class GroupDTO : IGroupDTO
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
        #endregion

        #region Explicit Interface Implementations
        [JsonIgnore]
        List<IContactDTO> IGroupDTO.Members
        {
            get => [.. Members.Cast<IContactDTO>()];
            set
            {
                Members = [.. value.Cast<ContactDTO>()];
                ModifiedDate = DateTime.Now;
            }
        }

        [JsonIgnore]
        IAccountDTO? IGroupDTO.Account { get => Account; set => Account = (AccountDTO)value!; }

        public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();

        public string ToJson() => throw new NotImplementedException();
        #endregion

        #region Properties
        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id { get; set; } = 0;

        [Required, JsonPropertyName("name")]
        public string? GroupName { get; set; } = null;

        [Required, JsonPropertyName("description")]
        public string? Description { get; set; } = null;

        [Required, JsonPropertyName("members")]
        public List<ContactDTO> Members { get; set; } = [];

        [JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account Id must be a non-negative number."), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int AccountId { get; set; } = 0;

        [JsonPropertyName("account"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AccountDTO? Account { get; set; } = null;

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = default;
        #endregion
    }
}