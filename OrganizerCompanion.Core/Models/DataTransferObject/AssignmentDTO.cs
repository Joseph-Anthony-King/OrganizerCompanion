using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class AssignmentDTO : IAssignmentDTO
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string _name = string.Empty;
        private string? _description = null;
        private List<GroupDTO>? _groups = null;
        private bool _isCompleted = false;
        private DateTime? _dateDue = null;
        private DateTime? _dateCompleted = null;
        private readonly DateTime _dateCreated = DateTime.UtcNow;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        [JsonIgnore]
        List<IGroupDTO>? IAssignmentDTO.Groups
        {
            get => [.. _groups!.Cast<IGroupDTO>()];
            set => _groups = value!.ConvertAll(group => (GroupDTO)group);
        }
        [JsonIgnore]
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Id), "Id must be a non-negative number.");
                _id = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("name"), MinLength(1, ErrorMessage = "Name must be at least 1 character long."), MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name
        {
            get => _name;
            set
            {
                if (value.Length < 1 || value.Length > 100)
                    throw new ArgumentException("Name must be between 1 and 100 characters long.", nameof(Name));
                _name = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [JsonPropertyName("description"), MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description
        {
            get => _description;
            set
            {
                if (value != null && value.Length > 1000)
                    throw new ArgumentException("Description cannot exceed 1000 characters.", nameof(Description));
                _description = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [JsonPropertyName("groups")]
        public List<GroupDTO>? Groups
        {
            get => _groups;
            set
            {
                _groups ??= [];
                _groups = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("isCompleted")]
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;

                _dateCompleted = value ? DateTime.Now : null;

                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateDue")]
        public DateTime? DateDue
        {
            get => _dateDue;
            set
            {
                _dateDue = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("dateCompleted")]
        public DateTime? DateCompleted
        {
            get => _dateCompleted;
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated
        {
            get => _dateCreated;
        }

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Constructors
        public AssignmentDTO() { }

        [JsonConstructor]
        public AssignmentDTO(
            int id,
            string name, 
            string? description, 
            List<GroupDTO>? groups, 
            bool isCompleted, 
            DateTime? dateDue,
            DateTime? dateCompleted,
            DateTime dateCreated,
            DateTime? dateModified,
            bool? isCast = null,
            int? castId = null,
            string? castType = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Groups = groups;
            IsCompleted = isCompleted;
            _dateDue = dateDue;
            _dateCompleted = dateCompleted;
            _dateCreated = dateCreated;
            DateModified = dateModified;
            IsCast = isCast ?? false;
            CastId = castId ?? 0;
            CastType = castType;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        #endregion
    }
}
