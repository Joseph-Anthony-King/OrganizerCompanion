using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class Assignment : IAssignment
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string _name = string.Empty;
        private string? _description = null;
        private List<Group>? _groups = null;
        private int? _taskId = null;
        private Task? _task = null;
        private bool _isCompleted = false;
        private DateTime? _dateDue = null;
        private DateTime? _dateCompleted = null;
        private DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        [JsonIgnore]
        List<IGroup>? IAssignment.Groups
        {
            get => [.. _groups!.Cast<IGroup>()];
            set => _groups = value!.ConvertAll(group => (Group)group);
        }
        [JsonIgnore]
        ITask? IAssignment.Task
        {
            get => _task;
            set => _task = (Task?)value;
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
                DateModified = DateTime.Now;
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
                DateModified = DateTime.Now;
            }
        }

        [JsonPropertyName("description"), MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description
        {
            get => _description!;
            set
            {
                if (value!.Length > 1000)
                    throw new ArgumentException("Description cannot exceed 1000 characters.", nameof(Description));
                _description = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("groups")]
        public List<Group>? Groups
        {
            get => _groups;
            set
            {
                _groups ??= [];
                _groups = value;
                DateModified = DateTime.Now;
            }
        }

        [JsonPropertyName("taskId"), Range(0, int.MaxValue, ErrorMessage = "Task Id must be a non-negative number.")]
        public int? TaskId
        {
            get => _taskId;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(TaskId), "Task Id must be a non-negative number.");
                _taskId = value;
                DateModified = DateTime.Now;
            }
        }

        [JsonPropertyName("task")]
        public Task? Task
        {
            get => _task;
            set
            {
                _task = value;
                DateModified = DateTime.Now;
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
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateCompleted")]
        public DateTime? DateCompleted
        {
            get => _dateCompleted;
            set
            {
                _dateCompleted = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated
        {
            get => _dateCreated;
            set
            {
                _dateCreated = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Constructors
        public Assignment() { }

        [JsonConstructor]
        public Assignment(
            int id,
            string name,
            string? description,
            List<Group>? groups,
            int? taskId,
            Task? task,
            bool isCompleted,
            DateTime? dateDue,
            DateTime? dateCompleted,
            DateTime dateCreated,
            DateTime? dateModified,
            bool? isCast = null,
            int? castId = null,
            string? castType = null)
        {
            _id = id;
            _name = name;
            _description = description;
            _groups = groups ?? [];
            _taskId = taskId;
            _task = task;
            _isCompleted = isCompleted;
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
            try
            {
                if (typeof(T) == typeof(AssignmentDTO) || typeof(T) == typeof(IAssignmentDTO))
                {
                    var dto = new AssignmentDTO(
                        Id,
                        Name,
                        Description,
                        Groups?.ConvertAll(group => group.Cast<GroupDTO>()),
                        TaskId,
                        Task,
                        IsCompleted,
                        DateDue,
                        DateCompleted,
                        DateCreated,
                        DateModified,
                        IsCast,
                        CastId,
                        CastType
                    );
                    
                    return (T)(object)dto;
                }
                else throw new InvalidCastException($"Cannot cast Feature to type {typeof(T).Name}.");
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
