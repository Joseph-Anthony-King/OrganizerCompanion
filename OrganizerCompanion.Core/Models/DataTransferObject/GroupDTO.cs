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

        #region Properties
        #region Explicit Interface Implementations
        [JsonIgnore]
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        List<IContactDTO> IGroupDTO.Members
        {
            get => [.. Members.Cast<IContactDTO>()];
            set
            {
                Members = [.. value.Cast<ContactDTO>()];
                DateModified = DateTime.Now;
            }
        }

        [JsonIgnore]
        IAccountDTO? IGroupDTO.Account { get => Account; set => Account = (AccountDTO)value!; }

        public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();

        public string ToJson() => throw new NotImplementedException();
        #endregion

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "ID must be a non-negative number")]
        public int Id { get; set; } = 0;
        [Required, JsonPropertyName("name")]
        public string? Name { get; set; } = null;
        [Required, JsonPropertyName("description")]
        public string? Description { get; set; } = null;
        [Required, JsonPropertyName("members")]
        public List<ContactDTO> Members { get; set; } = [];
        [JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account ID must be a non-negative number"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int AccountId { get; set; } = 0;
        [JsonPropertyName("account"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AccountDTO? Account { get; set; } = null;
        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;
        #endregion
    }
}