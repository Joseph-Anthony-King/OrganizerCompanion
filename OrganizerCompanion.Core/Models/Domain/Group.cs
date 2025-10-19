using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class Group : IGroup
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
        List<IContact> IGroup.Members
        {
            get => [.. Members.Cast<IContact>()];
            set
            {
                Members = [.. value.Cast<Contact>()];
                DateModified = DateTime.Now;
            }
        }
        [JsonIgnore]
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id { get; set; } = 0;
        [Required, JsonPropertyName("name")]
        public string? Name { get; set; } = null;
        [Required, JsonPropertyName("description")]
        public string? Description { get; set; } = null;
        [Required, JsonPropertyName("members")]
        public List<Contact> Members { get; set; } = [];
        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account Id must be a non-negative number.")]
        public int AccountId { get; set; } = 0;
        [Required, JsonPropertyName("account")]
        public IAccount? Account { get; set; } = null;
        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Constructors
        public Group() { }

        [JsonConstructor]
        public Group(
            int id,
            string? name,
            string? description,
            List<Contact> members,
            int accountId,
            IAccount? account,
            DateTime dateCreated,
            DateTime? dateModified,
            bool isCast = false,
            int castId = 0,
            string? castType = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Members = members;
            AccountId = accountId;
            Account = account;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(GroupDTO) || typeof(T) == typeof(IGroupDTO))
                {
                    object dto = new GroupDTO
                    {
                        Id = this.Id,
                        Name = this.Name,
                        Description = this.Description,
                        Members = this.Members.ConvertAll(member => member.Cast<ContactDTO>()),
                        AccountId = this.AccountId,
                        Account = (AccountDTO?)this.Account,
                        DateCreated = this.DateCreated,
                        DateModified = this.DateModified
                    };
                    return (T)dto;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast Group to {typeof(T).Name}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        #endregion
    }
}