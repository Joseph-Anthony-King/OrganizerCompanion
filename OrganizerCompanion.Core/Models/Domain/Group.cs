using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        private int _id = 0;
        private string? _groupName = null;
        private string? _description = null;
        private List<Contact> _members = [];
        private int _accountId = 0;
        private Account? _account = null;
        private DateTime _createdDate = DateTime.UtcNow;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        [JsonIgnore]
        List<IContact>? IGroup.Members
        {
            get => [.. Members.Cast<IContact>()];
            set
            {
                Members = [.. value!.Cast<Contact>()];
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [JsonIgnore]
        IAccount? IGroup.Account
        {
            get => Account;
            set
            {
                Account = (Account)value!;
                ModifiedDate = DateTime.UtcNow;
            }
        }
        #endregion

        [Key]
        [Column("GroupId")]
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

        [Required, JsonPropertyName("name")]
        public string? GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("description")]
        public string? Description
        {
            get => _description;
            set
            {
                _description = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("members")]
        public List<Contact> Members
        {
            get => _members;
            set
            {
                _members = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account Id must be a non-negative number.")]
        public int AccountId
        {
            get => _accountId;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Account Id must be a non-negative number.");
                }
                _accountId = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [ForeignKey("AccountId")]
        [Required, JsonPropertyName("account")]
        public Account? Account
        {
            get => _account;
            set
            {
                _account = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate
        {
            get => _createdDate;
            set => _createdDate = value;
        }

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;
        #endregion

        #region Constructors
        public Group() { }

        [JsonConstructor]
        public Group(
            int id,
            string? groupName,
            string? description,
            List<Contact> members,
            int accountId,
            Account? account,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            Id = id;
            GroupName = groupName;
            Description = description;
            Members = members;
            AccountId = accountId;
            Account = account;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public Group(
            string? groupName,
            string? description,
            List<Contact> members,
            int accountId,
            Account? account)
        {
            GroupName = groupName;
            Description = description;
            Members = members;
            AccountId = accountId;
            Account = account;
        }

        public Group(IGroupDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "DTO cannot be null.");
            }

            _id = dto.Id;
            _groupName = dto.GroupName;
            _description = dto.Description;
            _members = dto.Members.ConvertAll(member => new Contact(member));
            _accountId = dto.AccountId;
            _account = dto.Account != null ? new Account(dto.Account) : null;
            _createdDate = dto.CreatedDate;
            ModifiedDate = dto.ModifiedDate;
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
                        Id = Id,
                        GroupName = GroupName,
                        Description = Description,
                        Members = Members.ConvertAll(member => member.Cast<ContactDTO>()),
                        AccountId = AccountId,
                        Account = Account?.Cast<AccountDTO>(),
                        CreatedDate = CreatedDate,
                        ModifiedDate = ModifiedDate
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

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.Name:{1}", _id, _groupName);
        #endregion
    }
}
