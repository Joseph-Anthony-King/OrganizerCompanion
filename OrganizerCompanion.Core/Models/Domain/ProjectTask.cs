using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class ProjectTask : IProjectTask
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string _projectTaskName = string.Empty;
        private string? _description = null;
        private List<ProjectAssignment>? _assignments = null;
        private bool _isCompleted = false;
        private DateTime? _dueDate = null;
        private DateTime? _completedDate = null;
        private readonly DateTime _createdDate = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit ITaskDTO Implementation
        [JsonIgnore]
        List<IProjectAssignment>? IProjectTask.Assignments
        {
            get => _assignments?.Select(a => a.Cast<IProjectAssignment>()).ToList();
            set => _assignments = value?.Select(a => (ProjectAssignment)a).ToList();
        }
        #endregion

        [Key]
        [Column("ProjectTaskId")]
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

        [Required, JsonPropertyName("projectAssignmentName"), MinLength(1, ErrorMessage = "ProjectAssignmentName must be at least 1 character long."), MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string ProjectTaskName
        {
            get => _projectTaskName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name must be at least 1 character long.", nameof(ProjectTaskName));
                }

                if (value.Length > 100)
                {
                    throw new ArgumentException("Name cannot exceed 100 characters.", nameof(ProjectTaskName));
                }

                _projectTaskName = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("description"), MinLength(1, ErrorMessage = "Description must be at least 1 character long"), MaxLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
        public string? Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description must be at least 1 character long.", nameof(Description));
                }

                if (value.Length > 1000)
                {
                    throw new ArgumentException("Description cannot exceed 1000 characters.", nameof(Description));
                }

                _description = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("assignments")]
        public List<ProjectAssignment>? Assignments
        {
            get => _assignments;
            set
            {
                _assignments ??= [];
                _assignments = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("isCompleted")]
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                _completedDate = value ? DateTime.UtcNow : null;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("dueDate")]
        public DateTime? DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("completedDate")]
        public DateTime? CompletedDate => _completedDate;

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate => _createdDate;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;
        #endregion

        #region Constructors
        public ProjectTask() { }

        [JsonConstructor]
        public ProjectTask(
            int id,
            string name,
            string? description,
            List<ProjectAssignment>? assignments,
            bool isCompleted,
            DateTime? dueDate,
            DateTime? completedDate,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _projectTaskName = name;
            _description = description;
            _assignments = assignments;
            _isCompleted = isCompleted;
            _dueDate = dueDate;
            _completedDate = completedDate;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public ProjectTask(IProjectTaskDTO task)
        {
            _id = task.Id;
            _projectTaskName = task.ProjectTaskName;
            _description = task.Description;
            _assignments = task.Assignments?.Select(a => new ProjectAssignment(a)).ToList();
            _isCompleted = task.IsCompleted;
            _dueDate = task.DueDate;
            _completedDate = task.CompletedDate;
            _createdDate = task.CreatedDate;
            ModifiedDate = task.ModifiedDate;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(ProjectTaskDTO) || typeof(T) == typeof(IProjectTaskDTO))
                {
                    var dto = new ProjectTaskDTO
                    (
                        _id,
                        _projectTaskName,
                        _description,
                        _assignments?.Select(a => a.Cast<ProjectAssignmentDTO>()).ToList(),
                        _isCompleted,
                        _dueDate,
                        _completedDate,
                        _createdDate,
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

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.Name:{1}.IsCompleted:{2}", _id, _projectTaskName, _isCompleted);
        #endregion
    }
}
