using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class ProjectAssignment : IProjectAssignment
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string _projectAssignmentName = string.Empty;
        private string? _description = null;
        private SubAccount? _assignee = null;
        private int? _locationId = null;
        private string? _locationType = null;
        private IAddress? _location = null;
        private List<Group>? _groups = null;
        private int? _taskId = null;
        private ProjectTask? _task = null;
        private bool _isCompleted = false;
        private DateTime? _dueDate = null;
        private DateTime? _completedDate = null;
        private DateTime _createdDate = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        [JsonIgnore]
        ISubAccount? IProjectAssignment.Assignee
        {
            get => _assignee;
            set
            {
                _assignee = (SubAccount?)value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [JsonIgnore]
        IAddress? IProjectAssignment.Location
        {
            get => _location;
            set
            {
                _location = value!;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [JsonIgnore]
        List<IGroup>? IProjectAssignment.Groups
        {
            get => [.. _groups!.Cast<IGroup>()];
            set => _groups = value!.ConvertAll(group => (Group)group);
        }

        [JsonIgnore]
        IProjectTask? IProjectAssignment.Task
        {
            get => _task;
            set => _task = (ProjectTask?)value;
        }
        #endregion

        [Key]
        [Column("ProjectAssignmentId")]
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
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("projectAssignmentName"), MinLength(1, ErrorMessage = "ProjectAssignmentName must be at least 1 character long."), MaxLength(100, ErrorMessage = "ProjectAssignmentName cannot exceed 100 characters.")]
        public string ProjectAssignmentName
        {
            get => _projectAssignmentName;
            set
            {
                if (value.Length < 1 || value.Length > 100)
                {
                    throw new ArgumentException("ProjectAssignmentName must be between 1 and 100 characters long.", nameof(ProjectAssignmentName));
                }

                _projectAssignmentName = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [JsonPropertyName("description"), MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description
        {
            get => _description!;
            set
            {
                if (value!.Length > 1000)
                {
                    throw new ArgumentException("Description cannot exceed 1000 characters.", nameof(Description));
                }

                _description = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [JsonPropertyName("assigneeId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? AssigneeId
        {
            get => _assignee?.Id;
        }

        [ForeignKey(nameof(AssigneeId))]
        [JsonPropertyName("assignee"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SubAccount? Assignee
        {
            get => _assignee;
            set
            {
                _assignee = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [JsonPropertyName("locationId"), Range(0, int.MaxValue, ErrorMessage = "Location Id must be a non-negative number."), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? LocationId
        {
            get => _locationId;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(LocationId), "Location Id must be a non-negative number.");
                }

                _locationId = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [JsonPropertyName("locationType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LocationType
        {
            get => _locationType;
            set
            {
                _locationType = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [NotMapped]
        [JsonPropertyName("location"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IAddress? Location
        {
            get => _location;
            set
            {
                _location = value!;
                ModifiedDate = DateTime.Now;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("groups")]
        public List<Group>? Groups
        {
            get => _groups;
            set
            {
                _groups ??= [];
                _groups = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [JsonPropertyName("taskId"), Range(0, int.MaxValue, ErrorMessage = "Task Id must be a non-negative number.")]
        public int? TaskId
        {
            get => _taskId;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(TaskId), "Task Id must be a non-negative number.");
                }

                _taskId = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [ForeignKey(nameof(TaskId))]
        [JsonPropertyName("task")]
        public ProjectTask? Task
        {
            get => _task;
            set
            {
                _task = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("isCompleted")]
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;

                _completedDate = value ? DateTime.Now : null;

                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dueDate")]
        public DateTime? DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("completedDate")]
        public DateTime? CompletedDate
        {
            get => _completedDate;
            set
            {
                _completedDate = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate
        {
            get => _createdDate;
            set
            {
                _createdDate = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;
        #endregion

        #region Constructors
        public ProjectAssignment() { }

        [JsonConstructor]
        public ProjectAssignment(
            int id,
            string name,
            string? description,
            SubAccount? assignee,
            int? locationId,
            string? locationType,
            IAddress? location,
            List<Group>? groups,
            int? taskId,
            ProjectTask? task,
            bool isCompleted,
            DateTime? dueDate,
            DateTime? completedDate,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _projectAssignmentName = name;
            _description = description;
            _assignee = assignee;
            _locationId = locationId;
            _locationType = locationType;
            _location = location!;
            _groups = groups ?? [];
            _taskId = taskId;
            _task = task;
            _isCompleted = isCompleted;
            _dueDate = dueDate;
            _completedDate = completedDate;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public ProjectAssignment(IProjectAssignmentDTO assignment)
        {
            _id = assignment.Id;
            _projectAssignmentName = assignment.ProjectAssignmentName;
            _description = assignment.Description;
            _assignee = (SubAccount?)assignment.Assignee;
            _locationId = assignment.LocationId;
            _locationType = assignment.LocationType;
            _location = assignment.Location != null ? (IAddress)assignment.Location : null;
            _groups = assignment.Groups?.ConvertAll(group => (Group)group) ?? [];
            _taskId = assignment.TaskId;
            _task = (ProjectTask?)assignment.Task;
            _isCompleted = assignment.IsCompleted;
            _dueDate = assignment.DueDate;
            _completedDate = assignment.CompletedDate;
            _createdDate = assignment.CreatedDate;
            ModifiedDate = assignment.ModifiedDate;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(ProjectAssignmentDTO) || typeof(T) == typeof(IProjectAssignmentDTO))
                {
                    var dto = new ProjectAssignmentDTO(
                        Id,
                        ProjectAssignmentName,
                        Description,
                        Assignee?.Cast<SubAccountDTO>(),
                        LocationId,
                        LocationType,
                        Location?.Cast<IAddressDTO>(),
                        Groups?.ConvertAll(group => group.Cast<GroupDTO>()),
                        TaskId,
                        Task!.Cast<ProjectTaskDTO>(),
                        IsCompleted,
                        DueDate,
                        CompletedDate,
                        CreatedDate,
                        ModifiedDate
                    );

                    return (T)(object)dto;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast Feature to type {typeof(T).Name}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.Name:{1}.IsCompleted:{2}", _id, _projectAssignmentName, _isCompleted);
        #endregion
    }
}
